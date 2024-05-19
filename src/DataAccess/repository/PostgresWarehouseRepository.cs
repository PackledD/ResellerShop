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
using BuisnessLogic.controllers_inner;
using DataAccess.validator.creator;
using DataAccess.validator;
using Npgsql;

namespace DataAccess.repository
{
    public class PostgresWarehouseRepository : IWarehouseRepository
    {
        private PostgresWarehouseValidator valid;
        public PostgresWarehouseRepository()
        {
            valid = ValidatorCreator<PostgresWarehouseValidator>.Create();
        }

        private int GetNextId()
        {
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT max(id) + 1 FROM Warehouses");
            object res = con.Request(sql).ExecScalar();
            con.Close();
            if (res.GetType() == DBNull.Value.GetType())
            {
                return 1;
            }
            return Convert.ToInt32(res);
        }

        public void Add(Warehouse wareh)
        {
            wareh.Id = GetNextId();
            if (!valid.ValidateAdd(wareh))
            {
                throw new InsertException("Can't add Warehouse, already exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("INSERT INTO Warehouses\n" +
                                       "VALUES ({0})", wareh.ToString());
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            if (!valid.ValidateDelete(id))
            {
                throw new DeleteException("Can't delete Warehouse, not exists");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("DELETE FROM Warehouses\n" +
                                       "WHERE id = {0}",
                                       id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }

        private Warehouse Read(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                return new Warehouse(reader.GetInt32(0), reader.GetString(1));
            }
            return null;
        }

        private WareProd ReadWareProd(NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                ProductController c1 = new(new PostgresProductRepository());
                return new WareProd(Get(reader.GetInt32(1)), c1.Get(reader.GetInt32(0)),
                                    reader.GetInt32(2));
            }
            return null;
        }

        public Warehouse Get(int id)
        {
            if (!valid.ValidateGet(id))
            {
                throw new GetException("Can't get Warehouse");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Warehouses\n" +
                                       "WHERE id = {0}",
                                       id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            var res = Read(data);
            con.Close();
            return res;
        }

        public List<Warehouse> GetAll()
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all Warehouses");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM Warehouses");
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            Warehouse cur;
            List<Warehouse> res = new List<Warehouse>();
            while ((cur = Read(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public List<WareProd> GetProducts(Warehouse wh)
        {
            if (!valid.ValidateGetAll())
            {
                throw new GetException("Can't get all products in Warehouse");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("SELECT * FROM ProductsInWarehouses\n" +
                                       "WHERE warehouse_id = {0}",
                                       wh.Id);
            NpgsqlDataReader? data = con.Request(sql).ExecStream() as NpgsqlDataReader;
            WareProd cur;
            List<WareProd> res = new List<WareProd>();
            while ((cur = ReadWareProd(data)) != null)
            {
                res.Add(cur);
            }
            con.Close();
            return res;
        }

        public void Update(Warehouse wh)
        {
            if (!valid.ValidateUpdate(wh))
            {
                throw new UpdateException("Can't update Warehouse");
            }
            IDbConnector con = PostgresDbConnectorCreator.Create();
            string sql = string.Format("UPDATE Warehouses\n" +
                                       "SET addr = '{0}'\n" +
                                       "WHERE id = {1}",
                                       wh.Address, wh.Id);
            con.Request(sql).ExecNonQuery();
            con.Close();
        }
    }
}
