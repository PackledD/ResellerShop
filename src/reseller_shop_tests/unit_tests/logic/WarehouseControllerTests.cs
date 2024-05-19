using BuisnessLogic.enums;
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
    public class WarehouseControllerTests
    {
        [TestMethod]
        public void WarehouseCreateValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = It.IsAny<Warehouse>();

            ctrl.Create(wh);
            mock.Verify(c => c.Add(wh), Times.Once);
        }

        [TestMethod]
        public void WarehouseCreateException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = new Warehouse(0, "TestWarehouse");

            mock.Setup(c => c.Add(wh)).Throws(new TestException());
            Action act = () => ctrl.Create(wh);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(wh), Times.Once);
        }

        [TestMethod]
        public void WarehouseGetValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            int id = 0;

            mock.Setup(c => c.Get(id)).Returns(wh);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(wh, res);
        }

        [TestMethod]
        public void WarehouseGetException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void WarehouseGetAllValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = new List<Warehouse>
            {
                It.IsAny<Warehouse>(),
                It.IsAny<Warehouse>(),
                It.IsAny<Warehouse>()
            };

            mock.Setup(c => c.GetAll()).Returns(wh);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(wh, res);
        }

        [TestMethod]
        public void WarehouseGetAllException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void WarehouseGetProductsValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var prod = new List<WareProd>
            {
                It.IsAny<WareProd>(),
                It.IsAny<WareProd>(),
                It.IsAny<WareProd>()
            };
            var wh = new Warehouse(0, "TestWarehouse");

            mock.Setup(c => c.GetProducts(wh)).Returns(prod);
            var res = ctrl.GetProducts(wh);
            mock.Verify(c => c.GetProducts(wh), Times.Once);
            Assert.AreEqual(prod, res);
        }

        [TestMethod]
        public void WarehouseGetProductsException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = new Warehouse(0, "TestWarehouse");

            mock.Setup(c => c.GetProducts(wh)).Throws(new TestException());
            Action act = () => ctrl.GetProducts(wh);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetProducts(wh), Times.Once);
        }

        [TestMethod]
        public void WarehouseDeleteValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            int id = 0;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void WarehouseDeleteException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void WarehouseUpdateValid()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = It.IsAny<Warehouse>();

            ctrl.Update(wh);
            mock.Verify(c => c.Update(wh), Times.Once);
        }

        [TestMethod]
        public void WarehouseUpdateException()
        {
            var mock = new Mock<IWarehouseRepository>();
            var ctrl = new WarehouseController(mock.Object);
            var wh = new Warehouse(0, "TestWarehouse");

            mock.Setup(c => c.Update(wh)).Throws(new TestException());
            Action act = () => ctrl.Update(wh);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(wh), Times.Once);
        }
    }
}
