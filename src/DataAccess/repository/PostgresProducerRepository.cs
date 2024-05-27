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
using DataAccess.validator.creator;
using DataAccess.validator;
using BuisnessLogic.controllers_inner;

namespace DataAccess.repository
{
    public class PostgresProducerRepository : IProducerRepository
    {
        private PostgresProducerValidator valid;
        public PostgresProducerRepository()
        {
            valid = ValidatorCreator<PostgresProducerValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT next_id('Producers')");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            return Convert.ToInt32(res);
        }

        public void Add(Producer prod)
        {
            prod.Id = GetNextId();
            if (!valid.ValidateAdd(prod))
            {
                throw new InsertException("Can't add Producer, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Producers\n" +
                                       "VALUES ({0})", prod.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Producer, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM Producers\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Producer Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                ProducerController c = new(new PostgresProducerRepository());
                return new Producer(reader.GetInt32(0), reader.GetString(1));
            }
            return null;
        }

        public Producer Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Producer");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Producers\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Producer> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Producers");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Producers");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Producer cur;
            List<Producer> res = new List<Producer>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(Producer prod)
        {
            if (!valid.ValidateUpdate(prod))
            {
                throw new UpdateException("Can't update Producer");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE Producers\n" +
                                       "SET name = '{0}'\n" +
                                       "WHERE id = {1}",
                                       prod.Name, prod.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
