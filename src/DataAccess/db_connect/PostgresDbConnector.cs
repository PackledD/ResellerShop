using Npgsql;
using Exceptions.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.db_connect
{
    public class PostgresDbConnector : IDbConnector
    {
        private NpgsqlConnection con;
        private NpgsqlCommand? cmd;
        public PostgresDbConnector(string host, string db_name, DbUser user)
        {
            string cs = string.Format("Host={0};Username={1};Password={2};Database={3}",
                host, user.Username, user.Password, db_name);
            con = new NpgsqlConnection(cs);
            if (con == null)
            {
                throw new ConnectionException("Error while connect to DB");
            }
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Can't connect to DB");
            }
        }

        ~PostgresDbConnector()
        {
            Close();
        }

        public void Close()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public IDbConnector Request(string rq)
        {
            cmd = new NpgsqlCommand(rq, con);
            if (cmd == null)
            {
                throw new CmdException("Error while create request cmd");
            }
            return this;
        }

        public object? ExecScalar()
        {
            return cmd?.ExecuteScalar();
        }

        public object? ExecStream()
        {
            return cmd?.ExecuteReader();
        }

        public int? ExecNonQuery()
        {
            return cmd?.ExecuteNonQuery();
        }
    }
}
