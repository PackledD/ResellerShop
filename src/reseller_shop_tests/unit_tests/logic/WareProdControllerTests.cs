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
    public class WareProdControllerTests
    {
        [TestMethod]
        public void WareProdCreateValid()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            ctrl.Create(wp);
            mock.Verify(c => c.Add(wp), Times.Once);
        }

        [TestMethod]
        public void WareProdCreateException()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.Add(wp)).Throws(new TestException());
            Action act = () => ctrl.Create(wp);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(wp), Times.Once);
        }

        [TestMethod]
        public void WareProdGetValid()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.Get(0, 1)).Returns(wp);
            var res = ctrl.Get(0, 1);
            mock.Verify(c => c.Get(0, 1), Times.Once);
            Assert.AreEqual(wp, res);
        }

        [TestMethod]
        public void WareProdGetException()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.Get(0, 1)).Throws(new TestException());
            Action act = () => ctrl.Get(0, 1);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(0, 1), Times.Once);
        }

        [TestMethod]
        public void WareProdGetAllValid()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new List<WareProd>
            {
                new (wh, prod, 100),
                new (wh, prod, 150),
                new (wh, prod, 200)
            };

            mock.Setup(c => c.GetAll()).Returns(wp);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(wp, res);
        }

        [TestMethod]
        public void WareProdGetAllException()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void WareProdDeleteValid()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            ctrl.Delete(0, 1);
            mock.Verify(c => c.Delete(0, 1), Times.Once);
        }

        [TestMethod]
        public void WareProdDeleteException()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.Delete(0, 1)).Throws(new TestException());
            Action act = () => ctrl.Delete(0, 1);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(0, 1), Times.Once);
        }

        [TestMethod]
        public void WareProdUpdateValid()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            ctrl.Update(wp);
            mock.Verify(c => c.Update(wp), Times.Once);
        }

        [TestMethod]
        public void WareProdUpdateException()
        {
            var mock = new Mock<IWareProdRepository>();
            var ctrl = new WareProdController(mock.Object);
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var wp = new WareProd(wh, prod, 100);

            mock.Setup(c => c.Update(wp)).Throws(new TestException());
            Action act = () => ctrl.Update(wp);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(wp), Times.Once);
        }
    }
}
