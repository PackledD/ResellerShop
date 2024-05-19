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
    public class PostgresWarehouseValidator
    {
        public PostgresWarehouseValidator()
        {
        }

        private bool IsExists(Warehouse wh)
        {
            return IsExists(wh.Id);
        }

        private bool IsExists(int warehouseId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Warehouses\n" +
                                           "WHERE id = {0}",
                                           warehouseId);
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
                string sql = string.Format("SELECT * FROM Warehouses");
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

        public bool ValidateAdd(Warehouse wh)
        {
            return !IsExists(wh);
        }

        public bool ValidateDelete(int warehouseId)
        {
            return IsExists(warehouseId);
        }

        public bool ValidateGet(int warehouseId)
        {
            return IsExists(warehouseId);
        }

        public bool ValidateUpdate(Warehouse wh)
        {
            return IsExists(wh);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }

        public bool ValidateGetProducts(Warehouse wh)
        {
            return IsExists(wh);
        }
    }
}
