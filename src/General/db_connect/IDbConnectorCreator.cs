using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.db_connect
{
    public interface IDbConnectorCreator
    {
        IDbConnector Create();
    }
}
