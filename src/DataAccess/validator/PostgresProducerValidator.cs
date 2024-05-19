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
    public class PostgresProducerValidator
    {
        public PostgresProducerValidator()
        {
        }

        private bool IsExists(Producer cat)
        {
            return IsExists(cat.Id);
        }

        private bool IsExists(int producerId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Producers\n" +
                                           "WHERE id = {0}",
                                           producerId);
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
                string sql = string.Format("SELECT * FROM Producers");
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

        public bool ValidateAdd(Producer prod)
        {
            return !IsExists(prod);
        }

        public bool ValidateDelete(int producerId)
        {
            return IsExists(producerId);
        }

        public bool ValidateGet(int producerId)
        {
            return IsExists(producerId);
        }

        public bool ValidateUpdate(Producer prod)
        {
            return IsExists(prod);
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }
    }
}
