using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.db_connect
{
    public interface IDbConnector
    {
        IDbConnector Request(string rq);
        public object? ExecScalar();
        public object? ExecStream();
        public int? ExecNonQuery();

        public void Close();

    }
}
