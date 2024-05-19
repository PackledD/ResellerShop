using General.db_connect;
using DataAccess.repository;
using BuisnessLogic.interfaces.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.config
{
    public class PostgresConfig : IConfig
    {
        private string dbName;
        private string dbHost;
        private DbUser dbUser;
        public PostgresConfig(string dbName, string dbHost, DbUser dbUser)
        {
            this.dbName = dbName;
            this.dbHost = dbHost;
            this.dbUser = dbUser;
        }

        public DbUser GetDbUser()
        {
            return dbUser;
        }

        public string GetDbHost()
        {
            return dbHost;
        }

        public string GetDbName()
        {
            return dbName;
        }

        public IProductCategoryRepository GetProductCategoryRepository()
        {
            return new PostgresProductCategoryRepository();
        }

        public IContractRepository GetContractRepository()
        {
            return new PostgresContractRepository();
        }

        public IContractPosRepository GetContractPosRepository()
        {
            return new PostgresContractPosRepository();
        }

        public IFirmRepository GetFirmRepository()
        {
            return new PostgresFirmRepository();
        }

        public IProductRepository GetProductRepository()
        {
            return new PostgresProductRepository();
        }

        public IUserRepository GetUserRepository()
        {
            return new PostgresUserRepository();
        }

        public IWarehouseRepository GetWarehouseRepository()
        {
            return new PostgresWarehouseRepository();
        }

        public IWareProdRepository GetWareProdRepository()
        {
            return new PostgresWareProdRepository();
        }

        public IProducerRepository GetProducerRepository()
        {
            return new PostgresProducerRepository();
        }
    }
}
