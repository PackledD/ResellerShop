using Exceptions;
using General.db_connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config
{
    public class PostgresConfigCreator : IConfigCreator
    {
        private static PostgresConfig instance;

        public static PostgresConfig Create(string dbName, string dbHost, DbUser dbUser)
        {
            if (instance == null)
            {
                instance = new PostgresConfig(dbName, dbHost, dbUser);
            }
            return instance;
        }
        public static PostgresConfig Create()
        {
            if (instance == null)
            {
                throw new Exception("Config hasn't load yet");
            }
            return instance;
        }

        public static PostgresConfig Create(string path)
        {
            var data = ConfigLoader.Load(path, "postgres");
            DbUser user = new(data.UserLogin, data.UserPassword);
            return Create(data.DbName, data.DbHost, user);
        }

        IConfig IConfigCreator.Create()
        {
            return Create();
        }

        IConfig IConfigCreator.Create(string dbName, string dbHost, DbUser dbUser)
        {
            return Create(dbName, dbHost, dbUser);
        }

        IConfig IConfigCreator.Create(string path)
        {
            return Create(path);
        }
    }
}
