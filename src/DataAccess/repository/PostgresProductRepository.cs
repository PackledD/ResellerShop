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
using DataAccess.validator.creator;
using DataAccess.validator;

namespace DataAccess.repository
{
    public class PostgresProductRepository : IProductRepository
    {
        private PostgresProductValidator valid;
        public PostgresProductRepository()
        {
            valid = ValidatorCreator<PostgresProductValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM Products");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }

        public void Add(Product prod)
        {
            prod.Id = GetNextId();
            if (!valid.ValidateAdd(prod))
            {
                throw new InsertException("Can't add Product, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Products\n" +
                                       "VALUES ({0})", prod.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Product, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM Products\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Product Read(NpgsqlDataReader reader)
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

        public Product Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Product");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Products\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Product> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Products");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Products");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Product cur;
            List<Product> res = new List<Product>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public List<Product> GetByCategory(Category categ)
        {
            if (!valid.ValidateGetByCategory(categ))
            {
                throw new GetException("Can't get all Products by category");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Products\n" +
                                       "WHERE category = {0}",
                                       categ.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Product cur;
            List<Product> res = new List<Product>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(Product prod)
        {
            if (!valid.ValidateUpdate(prod))
            {
                throw new UpdateException("Can't update Product");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE Products\n" +
                                       "SET name = '{0}'," +
                                       "    category = {1}," +
                                       "    provider = {2}," +
                                       "    cost = {3}," +
                                       "    producer = '{4}'\n" +
                                       "WHERE id = {5}",
                                       prod.Name, prod.Category.Id, prod.Distributor.Id,
                                       prod.Cost, prod.Producer.Id, prod.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
