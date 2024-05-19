using General.config;
using Exceptions.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.db_connect
{
    public class PostgresDbConnectorCreator : IDbConnectorCreator
    {
        public static IDbConnector Create()
        {
            PostgresConfig cfg = PostgresConfigCreator.Create();
            return new PostgresDbConnector(cfg.GetDbHost(), cfg.GetDbName(), cfg.GetDbUser());
        }

        IDbConnector IDbConnectorCreator.Create()
        {
            return Create();
        }
    }
}
