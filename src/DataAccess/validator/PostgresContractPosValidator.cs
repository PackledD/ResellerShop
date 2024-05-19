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
    public class PostgresContractPosValidator
    {
        public PostgresContractPosValidator()
        {
        }

        private bool IsExists(ContractPos pos)
        {
            if (pos.Contract == null || pos.Warehouse == null || pos.Product == null)
            {
                return false;
            }
            return IsExists(pos.Contract.Id, pos.Warehouse.Id, pos.Product.Id);
        }

        private bool IsExists(int contractId, int wareId, int prodId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM ContractPositions\n" +
                                           "WHERE contract_id = {0} and warehouse_id = {1} and product_id = {2}",
                                           contractId, wareId, prodId);
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
                string sql = string.Format("SELECT * FROM ContractPositions");
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

        public bool ValidateAdd(ContractPos pos)
        {
            return !IsExists(pos);
        }

        public bool ValidateDelete(int contractId, int wareId, int prodId)
        {
            return IsExists(contractId, wareId, prodId);
        }

        public bool ValidateGet(int contractId, int wareId, int prodId)
        {
            return IsExists(contractId, wareId, prodId);
        }
        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }
    }
}
