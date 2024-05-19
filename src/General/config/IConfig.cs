using General.db_connect;
using BuisnessLogic.interfaces.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config
{
    public interface IConfig
    {
        DbUser GetDbUser();

        string GetDbHost();

        string GetDbName();

        IProductCategoryRepository GetProductCategoryRepository();

        IContractRepository GetContractRepository();

        IContractPosRepository GetContractPosRepository();

        IFirmRepository GetFirmRepository();

        IProductRepository GetProductRepository();

        IUserRepository GetUserRepository();

        IWarehouseRepository GetWarehouseRepository();

        IWareProdRepository GetWareProdRepository();

        IProducerRepository GetProducerRepository();
    }
}
