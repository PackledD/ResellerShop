using DataAccess.validator;
using DataAccess.validator.creator;
using General.config;
using General.db_connect;
using Exceptions.db;
using BuisnessLogic.controllers_inner;
using BuisnessLogic.models;
using BuisnessLogic.interfaces.repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataAccess.repository
{
    public class PostgresContractPosRepository : IContractPosRepository
    {
        private PostgresContractPosValidator valid;
        public PostgresContractPosRepository()
        {
            valid = ValidatorCreator<PostgresContractPosValidator>.Create();
        }

        public void Add(ContractPos pos)
        {
            if (!valid.ValidateAdd(pos))
            {
                throw new InsertException("Can't add ContractPos, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO ContractPositions\n" +
                                       "VALUES ({0})", pos.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int contractId, int wareId, int prodId)
        {
            if (!valid.ValidateDelete(contractId, wareId, prodId))
            {
                throw new DeleteException("Can't delete ContractPos, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM ContractPositions\n" +
                                       "WHERE contract_id = {0} and warehouse_id = {1} and product_id = {2}",
                                       contractId, wareId, prodId);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private ContractPos Read(NpgsqlDataReader reader)
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

        public ContractPos Get(int contractId, int wareId, int prodId)
        {
            if (!valid.ValidateGet(contractId, wareId, prodId))
            {
                throw new GetException("Can't get ContractPos");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ContractPositions\n" +
                                       "WHERE contract_id = {0} and warehouse_id = {1} and product_id = {2}",
                                       contractId, wareId, prodId);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<ContractPos> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all ContractPos");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ContractPositions");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            ContractPos cur;
            List<ContractPos> res = new List<ContractPos>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }
    }
}
