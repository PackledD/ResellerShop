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
    public class FirmControllerTests
    {
        [TestMethod]
        public void FirmCreateValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = It.IsAny<Firm>();

            ctrl.Create(firm);
            mock.Verify(c => c.Add(firm), Times.Once);
        }

        [TestMethod]
        public void FirmCreateException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.Add(firm)).Throws(new TestException());
            Action act = () => ctrl.Create(firm);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(firm), Times.Once);
        }

        [TestMethod]
        public void FirmGetValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = It.IsAny<Firm>();
            int id = 0;

            mock.Setup(c => c.Get(id)).Returns(firm);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(firm, res);
        }

        [TestMethod]
        public void FirmGetException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void FirmGetAllValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = new List<Firm>
            {
                It.IsAny<Firm>(),
                It.IsAny<Firm>(),
                It.IsAny<Firm>()
            };

            mock.Setup(c => c.GetAll()).Returns(firm);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(firm, res);
        }

        [TestMethod]
        public void FirmGetAllException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void FirmGetProductsValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var prod = new List<Product>
            {
                It.IsAny<Product>(),
                It.IsAny<Product>(),
                It.IsAny<Product>()
            };
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.GetProducts(firm)).Returns(prod);
            var res = ctrl.GetProducts(firm);
            mock.Verify(c => c.GetProducts(firm), Times.Once);
            Assert.AreEqual(prod, res);
        }

        [TestMethod]
        public void FirmGetProductsException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.GetProducts(firm)).Throws(new TestException());
            Action act = () => ctrl.GetProducts(firm);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetProducts(firm), Times.Once);
        }

        [TestMethod]
        public void FirmGetStaffValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var staff = new List<User>
            {
                It.IsAny<User>(),
                It.IsAny<User>(),
                It.IsAny<User>()
            };
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.GetStaff(firm)).Returns(staff);
            var res = ctrl.GetStaff(firm);
            mock.Verify(c => c.GetStaff(firm), Times.Once);
            Assert.AreEqual(staff, res);
        }

        [TestMethod]
        public void FirmGetStaffException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.GetStaff(firm)).Throws(new TestException());
            Action act = () => ctrl.GetStaff(firm);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetStaff(firm), Times.Once);
        }

        [TestMethod]
        public void FirmDeleteValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = It.IsAny<Firm>();
            int id = 0;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void FirmDeleteException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void FirmUpdateValid()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = It.IsAny<Firm>();

            ctrl.Update(firm);
            mock.Verify(c => c.Update(firm), Times.Once);
        }

        [TestMethod]
        public void FirmUpdateException()
        {
            var mock = new Mock<IFirmRepository>();
            var ctrl = new FirmController(mock.Object);
            var firm = new Firm(0, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");

            mock.Setup(c => c.Update(firm)).Throws(new TestException());
            Action act = () => ctrl.Update(firm);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(firm), Times.Once);
        }
    }
}
