using General.config;
using General.db_connect;
using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.db;
using Npgsql;
using BuisnessLogic.controllers_inner;
using BuisnessLogic.enums;
using DataAccess.validator.creator;
using DataAccess.validator;

namespace DataAccess.repository
{
    public class PostgresFirmRepository : IFirmRepository
    {
        private PostgresFirmValidator valid;
        public PostgresFirmRepository()
        {
            valid = ValidatorCreator<PostgresFirmValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM Firms");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }
        public void Add(Firm firm)
        {
            firm.Id = GetNextId();
            if (!valid.ValidateAdd(firm))
            {
                throw new InsertException("Can't add Firm, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Firms\n" +
                                       "VALUES ({0})", firm.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Firm, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM Firms\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Firm Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Firm(reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(3), reader.GetString(2),
                                reader.GetString(4), reader.GetString(5));
            }
            return null;
        }

        private Product ReadProduct(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                CategoryController c1 = new(new PostgresProductCategoryRepository());
                FirmController c2 = new(new PostgresFirmRepository());
                ProducerController c3 = new(new PostgresProducerRepository());
                return new Product(reader.GetInt32(0), reader.GetString(1),
                                   c1.Get(reader.GetInt32(2)), c2.Get(reader.GetInt32(3)),
                                   reader.GetInt32(4), c3.Get(reader.GetInt32(5)));
            }
            return null;
        }

        private User ReadUser(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                FirmController c1 = new(new PostgresFirmRepository());
                return new User(reader.GetInt32(0), reader.GetString(1),
                                c1.Get(reader.GetInt32(2)), reader.GetString(3),
                                reader.GetString(4), (UsersEnum)reader.GetInt32(5));
            }
            return null;
        }

        private Contract ReadContract(NpgsqlDataReader reader)
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

        public Firm Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Firm");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Firms\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Firm> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Firms");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Firms");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Firm cur;
            List<Firm> res = new List<Firm>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public List<Product> GetProducts(Firm firm)
        {
            if (!valid.ValidateGetProducts(firm))
            {
                throw new GetException("Can't get products of Firm");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Products\n" +
                                       "WHERE provider = {0}",
                                       firm.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Product cur;
            List<Product> res = new List<Product>();
            while ((cur = ReadProduct(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public List<User> GetStaff(Firm firm)
        {
            if (!valid.ValidateGetProducts(firm))
            {
                throw new GetException("Can't get staff of Firm");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Users\n" +
                                       "WHERE firm = {0}",
                                       firm.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            User cur;
            List<User> res = new List<User>();
            while ((cur = ReadUser(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(Firm firm)
        {
            if (!valid.ValidateUpdate(firm))
            {
                throw new UpdateException("Can't update Firm");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE Firms\n" +
                                       "SET name = '{0}'," +
                                       "    phone = '{1}'," +
                                       "    email = '{2}'," +
                                       "    physical_addr = '{3}'," +
                                       "    legal_addr = '{4}'\n" +
                                       "WHERE id = {5}",
                                       firm.Name, firm.Phone, firm.Email,
                                       firm.PhysAddr, firm.LawAddr, firm.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public List<Contract> GetContracts(Firm firm)
        {
            if (!valid.ValidateGetContracts(firm))
            {
                throw new GetException("Can't get contracts of Firm");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Contracts\n" +
                                       "WHERE firm_id = {0}",
                                       firm.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Contract cur;
            List<Contract> res = new List<Contract>();
            while ((cur = ReadContract(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }
    }
}
