using Npgsql;
using DataAccess.validator.creator;
using General.db_connect;
using Exceptions.db;
using BuisnessLogic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.validator
{
    public class PostgresProductCategoryValidator
    {
        public PostgresProductCategoryValidator()
        {
        }

        private bool IsExists(Category cat)
        {
            return IsExists(cat.Id);
        }

        private bool IsExists(int categoryId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM ProductCategories\n" +
                                           "WHERE id = {0}",
                                           categoryId);
                NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
                var res = data != null && data.Read();
                con.Close();
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsExistsAny()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM ProductCategories");
                NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
                var res = data != null && data.Read();
                con.Close();
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidateAdd(Category cat)
        {
            return !IsExists(cat);
        }

        public bool ValidateDelete(int categoryId)
        {
            return IsExists(categoryId);
        }

        public bool ValidateGet(int categoryId)
        {
            return IsExists(categoryId);
        }

        public bool ValidateUpdate(Category cat)
        {
            return IsExists(cat);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }
    }
}
