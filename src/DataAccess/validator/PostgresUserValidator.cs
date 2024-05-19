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
    public class PostgresUserValidator
    {
        public PostgresUserValidator()
        {
        }

        private bool IsExists(User user)
        {
            if (user.Firm == null)
            {
                return false;
            }
            return IsExists(user.Id) || IsExists(user.Email);
        }

        private bool IsExists(int userId)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Users\n" +
                                           "WHERE id = {0}",
                                           userId);
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

        private bool IsExists(string email)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Users\n" +
                                           "WHERE email = '{0}'",
                                           email);
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

        private bool IsExists(int userId, string email)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT * FROM Users\n" +
                                           "WHERE email = '{0}' and id = {1}",
                                           email, userId);
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
                string sql = string.Format("SELECT * FROM Users");
                NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
                return data != null && data.Read();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidateAdd(User user)
        {
            return !IsExists(user);
        }

        public bool ValidateDelete(int userId)
        {
            return IsExists(userId);
        }

        public bool ValidateGet(int userId)
        {
            return IsExists(userId);
        }

        public bool ValidateUpdate(User user)
        {
            return IsExists(user.Id, user.Email) || (IsExists(user.Id) && !IsExists(user.Email));
        }

        public bool ValidateGetAll()
        {
            //return IsExistsAny();
            return true;
        }

        public bool ValidateAuth(User user)
        {
            return IsExists(user);
        }
    }
}
