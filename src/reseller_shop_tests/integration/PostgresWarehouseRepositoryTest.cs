using DataAccess.repository;
using Exceptions;
using Exceptions.logic;
using General.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace reseller_shop_tests.integration
{
    [TestClass]
    public class PostgresWarehouseRepositoryTest
    {
        IWarehouseRepository rep;
        public PostgresWarehouseRepositoryTest()
        {
            rep = new PostgresWarehouseRepository();
        }

        [TestMethod]
        public void ProductAddRemove()
        {
            // Создание, удаление и обновление склада
            PostgresConfigCreator.Create("resell_data", "localhost", new("admin", "pg_admin"));
            Warehouse wh = new Warehouse(0, "Old address");
            try
            {
                rep.Add(wh);
                var res1 = rep.Get(wh.Id);
                res1.Address = "New address";
                rep.Update(res1);
                var res2 = rep.Get(wh.Id);
                Assert.IsTrue(wh.Address == "Old address");
                Assert.IsTrue(res2.Address == "New address");
            }
            catch (Exception)
            {
                rep.Delete(wh.Id);
                throw;
            }
            rep.Delete(wh.Id);
        }
    }
}
