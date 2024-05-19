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
    public class PostgresWareProdValidator
    {
        public PostgresWareProdValidator()
        {
        }

        private bool IsExists(WareProd wp)
        {
            if (wp.Warehouse == null || wp.Product == null)
            {
                return false;
            }
            return IsExists(wp.Warehouse.Id, wp.Product.Id);
        }

        private bool IsExists(int wareId, int prodId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM ProductsInWarehouses\n" +
                                           "WHERE product_id = {0} and warehouse_id = {1}",
                                           prodId, wareId);
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
                string sql = string.Format("SELECT * FROM ProductsInWarehouses");
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

        public bool ValidateAdd(WareProd wp)
        {
            return !IsExists(wp);
        }

        public bool ValidateDelete(int wareId, int prodId)
        {
            return IsExists(wareId, prodId);
        }

        public bool ValidateGet(int wareId, int prodId)
        {
            return IsExists(wareId, prodId);
        }

        public bool ValidateUpdate(WareProd wp)
        {
            return IsExists(wp);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }
    }
}
