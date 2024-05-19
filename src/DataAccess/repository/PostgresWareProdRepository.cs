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
using DataAccess.validator.creator;
using DataAccess.validator;

namespace DataAccess.repository
{
    public class PostgresWareProdRepository : IWareProdRepository
    {
        private PostgresWareProdValidator valid;
        public PostgresWareProdRepository()
        {
            valid = ValidatorCreator<PostgresWareProdValidator>.Create();
        }

        public void Add(WareProd wareProd)
        {
            if (!valid.ValidateAdd(wareProd))
            {
                throw new InsertException("Can't add Product in Warehouse, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO ProductsInWarehouses\n" +
                                       "VALUES ({0})", wareProd.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int wareId, int prodId)
        {
            if (!valid.ValidateDelete(wareId, prodId))
            {
                throw new DeleteException("Can't delete Product in Warehouse, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM ProductsInWarehouses\n" +
                                       "WHERE product_id = {0} and warehouse_id = {1}",
                                       prodId, wareId);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private WareProd Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                WarehouseController c1 = new(new PostgresWarehouseRepository());
                ProductController c2 = new(new PostgresProductRepository());
                return new WareProd(c1.Get(reader.GetInt32(1)), c2.Get(reader.GetInt32(0)),
                                    reader.GetInt32(2));
            }
            return null;
        }

        public WareProd Get(int wareId, int prodId)
        {
            if (!valid.ValidateGet(wareId, prodId))
            {
                throw new GetException("Can't get Product in Warehouse");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ProductsInWarehouses\n" +
                                       "WHERE product_id = {0} and warehouse_id = {1}",
                                       prodId, wareId);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<WareProd> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Products in Warehouses");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ProductsInWarehouses");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            WareProd cur;
            List<WareProd> res = new List<WareProd>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(WareProd wareProd)
        {
            if (!valid.ValidateUpdate(wareProd))
            {
                throw new UpdateException("Can't update Product in Warehouse");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE ProductsInWarehouses\n" +
                                       "SET count = {0}\n" +
                                       "WHERE product_id = {1} and warehouse_id = {2}",
                                       wareProd.Amount, wareProd.Product.Id, wareProd.Warehouse.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
