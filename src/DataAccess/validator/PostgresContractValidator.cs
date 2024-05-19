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
    public class PostgresContractValidator
    {
        public PostgresContractValidator()
        {
        }

        private bool IsExists(Contract contract)
        {
            return IsExists(contract.Id);
        }

        private bool IsExists(int contractId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Contracts\n" +
                                           "WHERE id = {0}",
                                           contractId);
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
                string sql = string.Format("SELECT * FROM Contracts");
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

        public bool ValidateAdd(Contract contract)
        {
            return !IsExists(contract);
        }

        public bool ValidateDelete(int contractId)
        {
            return IsExists(contractId);
        }

        public bool ValidateGet(int contractId)
        {
            return IsExists(contractId);
        }

        public bool ValidateUpdate(Contract contract)
        {
            return IsExists(contract);
        }

        public bool ValidateGetAll()
        {
            return IsExistsAny();
        }

        public bool ValidateGetContent(Contract contract)
        {
            //return IsExists(contract);
            return true;
        }

        public bool ValidateApprove(Contract contract, User manager)
        {
            return IsExists(contract) && ValidatorCreator<PostgresUserValidator>.Create().ValidateGet(manager.Id) &&
                    manager.IsManager() && contract.CanApprove(manager);
        }

        public bool ValidateSign(Contract contract, User director)
        {
            return IsExists(contract) && ValidatorCreator<PostgresUserValidator>.Create().ValidateGet(director.Id) &&
                    director.IsDirector() && contract.CanSign(director);
        }
    }
}
