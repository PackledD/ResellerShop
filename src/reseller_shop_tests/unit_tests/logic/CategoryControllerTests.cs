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
    public class CategoryControllerTests
    {
        [TestMethod]
        public void CategoryCreateValid()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");

            ctrl.Create(cat);
            mock.Verify(c => c.Add(cat), Times.Once);
        }

        [TestMethod]
        public void CategoryCreateException()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");

            mock.Setup(c => c.Add(cat)).Throws(new TestException());
            Action act = () => ctrl.Create(cat);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(cat), Times.Once);
        }

        [TestMethod]
        public void CategoryGetValid()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");
            int id = 1;

            mock.Setup(c => c.Get(id)).Returns(cat);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(cat, res);
        }

        [TestMethod]
        public void CategoryGetException()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");
            int id = 1;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void CategoryGetAllValid()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new List<Category> { new (1, "Test Category") };

            mock.Setup(c => c.GetAll()).Returns(cat);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(cat, res);
        }

        [TestMethod]
        public void CategoryGetAllException()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void CategoryDeleteValid()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");
            int id = 1;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void CategoryDeleteException()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");
            int id = 1;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void CategoryUpdateValid()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");

            ctrl.Update(cat);
            mock.Verify(c => c.Update(cat), Times.Once);
        }

        [TestMethod]
        public void CategoryUpdateException()
        {
            var mock = new Mock<IProductCategoryRepository>();
            var ctrl = new CategoryController(mock.Object);
            var cat = new Category(1, "Test Category");

            mock.Setup(c => c.Update(cat)).Throws(new TestException());
            Action act = () => ctrl.Update(cat);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(cat), Times.Once);
        }
    }
}
