using Exceptions;
using Exceptions.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace reseller_shop_tests.unit_tests.logic
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void ProductCreateValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);

            ctrl.Create(prod);
            mock.Verify(c => c.Add(prod), Times.Once);
        }

        [TestMethod]
        public void ProductCreateException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);

            mock.Setup(c => c.Add(prod)).Throws(new TestException());
            Action act = () => ctrl.Create(prod);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(prod), Times.Once);
        }

        [TestMethod]
        public void ProductGetValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);
            int id = 1;

            mock.Setup(c => c.Get(id)).Returns(prod);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(prod, res);
        }

        [TestMethod]
        public void ProductGetException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);
            int id = 1;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void ProductGetAllValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new List<Product>
            {
                new (1, "Test Product1", cat, firm, 1000, producer),
                new (2, "Test Product2", cat, firm, 500, producer),
                new (3, "Test Product3", cat, firm, 800, producer)
            };

            mock.Setup(c => c.GetAll()).Returns(prod);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(prod, res);
        }

        [TestMethod]
        public void ProductGetAllException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void ProductDeleteValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);
            int id = 1;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void ProductDeleteException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);
            int id = 1;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void ProductUpdateValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);

            ctrl.Update(prod);
            mock.Verify(c => c.Update(prod), Times.Once);
        }

        [TestMethod]
        public void ProductUpdateException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category");
            var producer = new Producer(22, "Test Producer");
            var prod = new Product(1, "Test Product", cat, firm, 1000, producer);

            mock.Setup(c => c.Update(prod)).Throws(new TestException());
            Action act = () => ctrl.Update(prod);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(prod), Times.Once);
        }



        [TestMethod]
        public void ProductGetByCategoryValid()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat1 = new Category(11, "Test Category1");
            var cat2 = new Category(111, "Test Category2");
            var producer = new Producer(22, "Test Producer");
            var prod = new List<Product>
            {
                new (1, "Test Product1", cat1, firm, 1000, producer),
                new (2, "Test Product2", cat1, firm, 500, producer),
                new (3, "Test Product3", cat2, firm, 800, producer)
            };
            var prod_res = new List<Product>(prod);
            prod_res.RemoveAt(2);

            mock.Setup(c => c.GetByCategory(cat1)).Returns(prod_res);
            var res = ctrl.GetByCategory(cat1);
            mock.Verify(c => c.GetByCategory(cat1), Times.Once);
            Assert.AreEqual(prod_res, res);
        }

        [TestMethod]
        public void ProductGetByCategoryException()
        {
            var mock = new Mock<IProductRepository>();
            var ctrl = new ProductController(mock.Object);
            var firm = new Firm(12, "Test Firm", "test@mail.com", "0123456789", "Test Phys Address", "Test Law Address");
            var cat = new Category(11, "Test Category1");

            mock.Setup(c => c.GetByCategory(cat)).Throws(new TestException());
            Action act = () => ctrl.GetByCategory(cat);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetByCategory(cat), Times.Once);
        }
    }
}
