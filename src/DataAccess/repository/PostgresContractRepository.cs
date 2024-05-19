using General.config;
using General.db_connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using Exceptions.db;
using Npgsql;
using BuisnessLogic.controllers_inner;
using DataAccess.validator.creator;
using DataAccess.validator;
using Exceptions;
using System.Runtime.InteropServices;

namespace DataAccess.repository
{
    public class PostgresContractRepository : IContractRepository
    {
        private PostgresContractValidator valid;
        public PostgresContractRepository()
        {
            valid = ValidatorCreator<PostgresContractValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM Contracts");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }

        public void Add(Contract contract)
        {
            contract.Id = GetNextId();
            if (!valid.ValidateAdd(contract))
            {
                throw new InsertException("Can't add Contract, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Contracts " +
                                       "(id, firm_id, conclusion_date, expiration_date, document)\n" +
                                       "VALUES ({0})", contract.ToShortString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Approve(Contract contract, User manager)
        {
            if (!valid.ValidateApprove(contract, manager))
            {
                throw new ContractException("Can't approve Contract");
            }
            contract.Approve(manager);
            Update(contract);
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Contract, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM Contracts\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Contract Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                UserController c1 = new(new PostgresUserRepository());
                FirmController c2 = new(new PostgresFirmRepository());
                return new Contract(reader.GetInt32(0), c2.Get(reader.GetInt32(1)),
                                    reader.IsDBNull(2) ? null : c1.Get(reader.GetInt32(2)),
                                    reader.IsDBNull(3) ? null : c1.Get(reader.GetInt32(3)),
                                    reader.IsDBNull(4) ? null : c1.Get(reader.GetInt32(4)),
                                    reader.IsDBNull(5) ? null : c1.Get(reader.GetInt32(5)),
                                    reader.GetDateTime(6), reader.GetDateTime(7),
                                    reader.GetString(8));
            }
            return null;
        }

        private ContractPos ReadContractPos(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                ContractController c1 = new(new PostgresContractRepository());
                WarehouseController c2 = new(new PostgresWarehouseRepository());
                ProductController c3 = new(new PostgresProductRepository());
                return new ContractPos(c1.Get(reader.GetInt32(0)), c2.Get(reader.GetInt32(2)),
                                       c3.Get(reader.GetInt32(1)), reader.GetInt32(3));
            }
            return null;
        }

        public Contract Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Contract");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Contracts\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Contract> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Contract");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Contracts");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Contract cur;
            List<Contract> res = new List<Contract>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public List<ContractPos> GetContent(Contract contract)
        {
            if (!valid.ValidateGetContent(contract))
            {
                throw new GetException("Can't get content of Contract");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ContractPositions\n" +
                                       "WHERE contract_id = {0}",
                                       contract.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            ContractPos cur;
            List<ContractPos> res = new List<ContractPos>();
            while ((cur = ReadContractPos(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Sign(Contract contract, User director)
        {
            if (!valid.ValidateSign(contract, director))
            {
                throw new ContractException("Can't sign Contract");
            }
            contract.Sign(director);
            Update(contract);
            if (contract.IsComplete())
            {
                ApplyContract(contract);
            }
        }

        private void ApplyContract(Contract contract)
        {
            ProductController c2 = new(new PostgresProductRepository());
            FirmController c3 = new(new PostgresFirmRepository());
            Firm distr = c3.Get(0);
            var lst = GetContent(contract);
            foreach (var elem in lst)
            {
                var prod = elem.Product;
                Product prodOur = null;
                bool cond = prod.Distributor.Id != 0;
                if (cond)
                {
                    prodOur = new(-1, prod.Name, prod.Category, distr, prod.Cost, prod.Producer);
                    c2.Create(prodOur);
                }
                ApplyContractPosition(elem, prod, true);
                if (prodOur != null)
                {
                    ApplyContractPosition(elem, prodOur, false);
                }
            }
        }

        private void ApplyContractPosition(ContractPos pos, Product prod, bool subtract)
        {
            WareProdController c = new(new PostgresWareProdRepository());
            WareProd wp = null;
            try
            {
                wp = c.Get(pos.Warehouse.Id, prod.Id);
                wp.Amount += subtract ? -pos.Amount : pos.Amount;
                c.Update(wp);
            }
            catch (Exception)
            {
                wp = new WareProd(pos.Warehouse, prod, subtract ? -pos.Amount : pos.Amount);
                c.Create(wp);
            }
        }

        public void Update(Contract contract)
        {
            if (!valid.ValidateUpdate(contract))
            {
                throw new UpdateException("Can't update Contract");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            User? u1 = contract.Director1,
                  u2 = contract.Director2,
                  u3 = contract.Manager1,
                  u4 = contract.Manager2;
            string sql = string.Format("UPDATE Contracts\n" +
                                       "SET firm_id = {0}," +
                                       "    director1_id = {1}," +
                                       "    director2_id = {2}," +
                                       "    manager1_id = {3}," +
                                       "    manager2_id = {4}," +
                                       "    conclusion_date = '{5}'," +
                                       "    expiration_date = '{6}'," +
                                       "    document = '{7}'\n" +
                                       "WHERE id = {8}",
                                       contract.Firm.Id, u1 != null ? u1.Id : "null", u2 != null ? u2.Id : "null",
                                       u3 != null ? u3.Id : "null", u4 != null ? u4.Id : "null", contract.ConclusionDate,
                                       contract.ExpirationDate, contract.Document, contract.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
