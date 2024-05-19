using General.db_connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config
{
    public interface IConfigCreator
    {
        IConfig Create(string path);
        IConfig Create(string dbName, string dbHost, DbUser dbUser);
        IConfig Create();
    }
}
