using DataAccess.validator.creator;
using Npgsql;
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
    public class PostgresProductValidator
    {
        public PostgresProductValidator()
        {
        }

        private bool IsExists(Product prod)
        {
            if (prod.Category == null || prod.Distributor == null)
            {
                return false;
            }
            return IsExists(prod.Id);
        }

        private bool IsExists(int productId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Products\n" +
                                           "WHERE id = {0}",
                                           productId);
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
                string sql = string.Format("SELECT * FROM Products");
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

        public bool ValidateAdd(Product prod)
        {
            return !IsExists(prod);
        }

        public bool ValidateDelete(int productId)
        {
            return IsExists(productId);
        }

        public bool ValidateGet(int productId)
        {
            return IsExists(productId);
        }

        public bool ValidateUpdate(Product prod)
        {
            return IsExists(prod);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }

        public bool ValidateGetByCategory(Category cat)
        {
            return ValidatorCreator<PostgresProductCategoryValidator>.Create().ValidateGet(cat.Id);
        }
    }
}
