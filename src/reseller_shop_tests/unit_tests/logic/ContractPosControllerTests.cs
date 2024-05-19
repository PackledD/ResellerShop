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
    public class ContractPosControllerTests
    {
        [TestMethod]
        public void ContractPosCreateValid()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            ctrl.Create(cp);
            mock.Verify(c => c.Add(cp), Times.Once);
        }

        [TestMethod]
        public void ContractPosCreateException()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            mock.Setup(c => c.Add(cp)).Throws(new TestException());
            Action act = () => ctrl.Create(cp);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(cp), Times.Once);
        }

        [TestMethod]
        public void ContractPosGetValid()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            mock.Setup(c => c.Get(0, 1, 2)).Returns(cp);
            var res = ctrl.Get(0, 1, 2);
            mock.Verify(c => c.Get(0, 1, 2), Times.Once);
            Assert.AreEqual(cp, res);
        }

        [TestMethod]
        public void ContractPosGetException()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            mock.Setup(c => c.Get(0, 1, 2)).Throws(new TestException());
            Action act = () => ctrl.Get(0, 1, 2);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(0, 1, 2), Times.Once);
        }

        [TestMethod]
        public void ContractPosGetAllValid()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new List<ContractPos>
            {
                new (contr, wh, prod, 50),
                new (contr, wh, prod, 60)
            };

            mock.Setup(c => c.GetAll()).Returns(cp);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(cp, res);
        }

        [TestMethod]
        public void ContractPosGetAllException()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void ContractPosDeleteValid()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            ctrl.Delete(0, 1, 2);
            mock.Verify(c => c.Delete(0, 1, 2), Times.Once);
        }

        [TestMethod]
        public void ContractPosDeleteException()
        {
            var mock = new Mock<IContractPosRepository>();
            var ctrl = new ContractPosController(mock.Object);
            var contr = It.IsAny<Contract>();
            var wh = It.IsAny<Warehouse>();
            var prod = It.IsAny<Product>();
            var cp = new ContractPos(contr, wh, prod, 50);

            mock.Setup(c => c.Delete(0, 1, 2)).Throws(new TestException());
            Action act = () => ctrl.Delete(0, 1, 2);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(0, 1, 2), Times.Once);
        }
    }
}
