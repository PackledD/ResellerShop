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
    public class PostgresProductRepositoryTest
    {
        IProductRepository rep;
        public PostgresProductRepositoryTest()
        {
            rep = new PostgresProductRepository();
        }

        [TestMethod]
        public void ProductAddRemove()
        {
            // Создание, удаление и обновление товара
            PostgresConfigCreator.Create("resell_data", "localhost", new("admin", "pg_admin"));
            IFirmRepository firm_rep = new PostgresFirmRepository();
            Firm firm1 = new(0, "test1", "test1@mail.ru", "1234", "test1", "test1");
            firm_rep.Add(firm1);
            IProductCategoryRepository cat_rep = new PostgresProductCategoryRepository();
            Category cat = new(0, "test cat");
            cat_rep.Add(cat);
            IProducerRepository prod_rep = new PostgresProducerRepository();
            Producer producer = new(0, "test producer");
            prod_rep.Add(producer);
            Product product = new Product(0, "Test product", cat_rep.Get(cat.Id), firm_rep.Get(firm1.Id), 10000, prod_rep.Get(producer.Id));
            try
            {
                rep.Add(product);
                var res1 = rep.Get(product.Id);
                res1.Cost += 1000;
                rep.Update(res1);
                var res2 = rep.Get(product.Id);
                Assert.IsTrue(product.Cost + 1000 == res2.Cost);
            }
            catch (Exception)
            {
                rep.Delete(product.Id);
                cat_rep.Delete(cat.Id);
                firm_rep.Delete(firm1.Id);
                throw;
            }
            rep.Delete(product.Id);
            cat_rep.Delete(cat.Id);
            firm_rep.Delete(firm1.Id);
        }
    }
}
