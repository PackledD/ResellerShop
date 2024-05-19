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
    public class PostgresFirmValidator
    {
        public PostgresFirmValidator()
        {
        }

        private bool IsExists(Firm firm)
        {
            return IsExists(firm.Id);
        }

        private bool IsExists(int firmId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Firms\n" +
                                           "WHERE id = {0}",
                                           firmId);
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
                string sql = string.Format("SELECT * FROM Firms");
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

        public bool ValidateAdd(Firm firm)
        {
            return !IsExists(firm);
        }

        public bool ValidateDelete(int firmId)
        {
            return IsExists(firmId);
        }

        public bool ValidateGet(int firmId)
        {
            return IsExists(firmId);
        }

        public bool ValidateUpdate(Firm firm)
        {
            return IsExists(firm);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }

        public bool ValidateGetProducts(Firm firm)
        {
            return IsExists(firm);
        }
        public bool ValidateGetContracts(Firm firm)
        {
            return IsExists(firm);
        }

        public bool ValidateGetStaff(Firm firm)
        {
            return IsExists(firm);
        }

        public bool ValidateAuth(Firm firm)
        {
            return IsExists(firm);
        }
    }
}
