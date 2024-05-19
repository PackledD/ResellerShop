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
using DataAccess.validator.creator;
using DataAccess.validator;

namespace DataAccess.repository
{
    public class PostgresProductCategoryRepository : IProductCategoryRepository
    {
        private PostgresProductCategoryValidator valid;
        public PostgresProductCategoryRepository()
        {
            valid = ValidatorCreator<PostgresProductCategoryValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM ProductCategories");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }

        public void Add(Category categ)
        {
            categ.Id = GetNextId();
            if (!valid.ValidateAdd(categ))
            {
                throw new InsertException("Can't add Category, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO ProductCategories\n" +
                                       "VALUES ({0})", categ.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Category, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM ProductCategories\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Category Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Category(reader.GetInt32(0), reader.GetString(1));
            }
            return null;
        }

        public Category Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Category");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ProductCategories\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Category> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Categories");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ProductCategories");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Category cur;
            List<Category> res = new List<Category>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(Category categ)
        {
            if (!valid.ValidateUpdate(categ))
            {
                throw new UpdateException("Can't update Category");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE ProductCategories\n" +
                                       "SET name = '{0}'\n" +
                                       "WHERE id = {1}",
                                       categ.Name, categ.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
