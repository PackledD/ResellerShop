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
using BuisnessLogic.controllers_inner;
using BuisnessLogic.enums;
using Exceptions.logic;
using Exceptions;
using DataAccess.validator.creator;
using DataAccess.validator;

namespace DataAccess.repository
{
    public class PostgresUserRepository : IUserRepository
    {
        private PostgresUserValidator valid;
        public PostgresUserRepository()
        {
            valid = ValidatorCreator<PostgresUserValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM Users");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }

        public void Add(User user)
        {
            user.Id = GetNextId();
            if (!valid.ValidateAdd(user))
            {
                throw new InsertException("Can't add User, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Users\n" +
                                       "VALUES ({0})", user.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }


        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete User, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM AuthData\n" +
                                       "WHERE user_id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            sql = string.Format("DELETE FROM Users\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private User Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                FirmController c1 = new(new PostgresFirmRepository());
                return new User(reader.GetInt32(0), reader.GetString(1),
                                c1.Get(reader.GetInt32(2)), reader.GetString(3),
                                reader.GetString(4), (UsersEnum)reader.GetInt32(5));
            }
            return null;
        }

        public User Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get User");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Users\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<User> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Users");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Users");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            User cur;
            List<User> res = new List<User>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(User user)
        {
            if (!valid.ValidateUpdate(user))
            {
                throw new UpdateException("Can't update User");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE Users\n" +
                                       "SET fullname = '{0}'," +
                                       "    firm = {1}," +
                                       "    email = '{2}'," +
                                       "    phone = '{3}'," +
                                       "    kind = {4}\n" +
                                       "WHERE id = {5}",
                                       user.FullName, user.Firm.Id, user.Email,
                                       user.Phone, (int)user.UserKind, user.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public User Auth(string login, string hash)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                string sql = string.Format("SELECT user_id FROM AuthData\n" +
                                           "WHERE user_id = (\n" +
                                           "    SELECT id FROM Users\n" +
                                           "    WHERE email = '{0}'" +
                                           ") and password_hash = '{1}'",
                                           login, hash);
                int? res = (int?)con.Request(sql).ExecScalar();
                con.Close();
                if (res != null)
                {
                    return Get((int)res);
                }
                else
                {
                    throw new UserException("Unknown user");
                }

            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new UserException("Error while authenticate");
            }
        }

        public User Register(User user, string hash)
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            try
            {
                Add(user);
                string sql = string.Format("INSERT INTO AuthData\n" +
                                           "VALUES ({0}, '{1}')", user.Id, hash);
                con.Request(sql).ExecNonQuery();
                con.Close();
                return user;
            }
            catch (LogicException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new UserException("Error while register");
            }
        }
    }
}
