using BuisnessLogic.enums;
using Exceptions;
using Exceptions.logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace reseller_shop_tests.unit_tests.logic
{
    [TestClass]
    public class ContractControllerTests
    {
        [TestMethod]
        public void ContractCreateValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = It.IsAny<Contract>();

            ctrl.Create(contr);
            mock.Verify(c => c.Add(contr), Times.Once);
        }

        [TestMethod]
        public void ContractCreateException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = new Contract(0, It.IsAny<Firm>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.Add(contr)).Throws(new TestException());
            Action act = () => ctrl.Create(contr);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(contr), Times.Once);
        }

        [TestMethod]
        public void ContractGetValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = It.IsAny<Contract>();
            int id = 0;

            mock.Setup(c => c.Get(id)).Returns(contr);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(contr, res);
        }

        [TestMethod]
        public void ContractGetException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void ContractGetAllValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = new List<Contract>
            {
                It.IsAny<Contract>(),
                It.IsAny<Contract>(),
                It.IsAny<Contract>()
            };

            mock.Setup(c => c.GetAll()).Returns(contr);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(contr, res);
        }

        [TestMethod]
        public void ContractGetAllException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void ContractGetContentValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var prod = new List<ContractPos>
            {
                It.IsAny<ContractPos>(),
                It.IsAny<ContractPos>(),
                It.IsAny<ContractPos>()
            };
            var contr = new Contract(0, It.IsAny<Firm>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.GetContent(contr)).Returns(prod);
            var res = ctrl.GetContent(contr);
            mock.Verify(c => c.GetContent(contr), Times.Once);
            Assert.AreEqual(prod, res);
        }

        [TestMethod]
        public void ContractGetContentException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = new Contract(0, It.IsAny<Firm>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.GetContent(contr)).Throws(new TestException());
            Action act = () => ctrl.GetContent(contr);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetContent(contr), Times.Once);
        }

        [TestMethod]
        public void ContractDeleteValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = It.IsAny<Contract>();
            int id = 0;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void ContractDeleteException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            int id = 0;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void ContractUpdateValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = It.IsAny<Contract>();

            ctrl.Update(contr);
            mock.Verify(c => c.Update(contr), Times.Once);
        }

        [TestMethod]
        public void ContractUpdateException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var contr = new Contract(0, It.IsAny<Firm>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.Update(contr)).Throws(new TestException());
            Action act = () => ctrl.Update(contr);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(contr), Times.Once);
        }

        [TestMethod]
        public void ContractApproveValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var manager = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Manager);

            ctrl.Approve(contr, manager);
            mock.Verify(c => c.Approve(contr, manager), Times.Once);
        }


        [TestMethod]
        public void ContractApproveInvalidByNotManager()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var manager = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Director);

            mock.Setup(c => c.Approve(contr, manager)).Throws(new TestException());
            Action act = () => ctrl.Approve(contr, manager);
            Assert.ThrowsException<InvalidUserException>(act);
            mock.Verify(c => c.Approve(contr, manager), Times.Never);
        }


        [TestMethod]
        public void ContractApproveInvalidByFirm()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm1 = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var firm2 = new Firm(2, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm1, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var manager = new User(0, "TestName", firm2, "test@email.com", "0123456789", UsersEnum.Manager);

            mock.Setup(c => c.Approve(contr, manager)).Throws(new TestException());
            Action act = () => ctrl.Approve(contr, manager);
            Assert.ThrowsException<InvalidUserException>(act);
            mock.Verify(c => c.Approve(contr, manager), Times.Never);
        }

        [TestMethod]
        public void ContractApproveInvalidByApproved()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var manager = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Manager);
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), manager,
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.Approve(contr, manager)).Throws(new TestException());
            Action act = () => ctrl.Approve(contr, manager);
            Assert.ThrowsException<AlreadyProcessedContractException>(act);
            mock.Verify(c => c.Approve(contr, manager), Times.Never);
        }

        [TestMethod]
        public void ContractApproveException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var manager = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Manager);

            mock.Setup(c => c.Approve(contr, manager)).Throws(new TestException());
            Action act = () => ctrl.Approve(contr, manager);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Approve(contr, manager), Times.Once);
        }

        [TestMethod]
        public void ContractSignValid()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var manager = new User(1, "TestManager", firm, "test", "test", UsersEnum.Manager);
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), manager, manager,
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var director = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Director);

            ctrl.Sign(contr, director);
            mock.Verify(c => c.Sign(contr, director), Times.Once);
        }


        [TestMethod]
        public void ContractSignInvalidByNotDirector()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var director = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Manager);

            mock.Setup(c => c.Sign(contr, director)).Throws(new TestException());
            Action act = () => ctrl.Sign(contr, director);
            Assert.ThrowsException<InvalidUserException>(act);
            mock.Verify(c => c.Sign(contr, director), Times.Never);
        }


        [TestMethod]
        public void ContractSignInvalidByFirm()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm1 = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var firm2 = new Firm(2, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var contr = new Contract(0, firm1, It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var director = new User(0, "TestName", firm2, "test@email.com", "0123456789", UsersEnum.Director);

            mock.Setup(c => c.Sign(contr, director)).Throws(new TestException());
            Action act = () => ctrl.Sign(contr, director);
            Assert.ThrowsException<InvalidUserException>(act);
            mock.Verify(c => c.Sign(contr, director), Times.Never);
        }

        [TestMethod]
        public void ContractSignInvalidByApproved()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var director = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Director);
            var contr = new Contract(0, firm, It.IsAny<User>(), director, It.IsAny<User>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);

            mock.Setup(c => c.Sign(contr, director)).Throws(new TestException());
            Action act = () => ctrl.Sign(contr, director);
            Assert.ThrowsException<AlreadyProcessedContractException>(act);
            mock.Verify(c => c.Sign(contr, director), Times.Never);
        }

        [TestMethod]
        public void ContractSignException()
        {
            var mock = new Mock<IContractRepository>();
            var ctrl = new ContractController(mock.Object);
            var firm = new Firm(1, "TestFirm", "test@email.com", "0123456789", "Test Phys Address", "Test Law Address");
            var manager = new User(1, "TestManager", firm, "test", "test", UsersEnum.Manager);
            var contr = new Contract(0, firm, It.IsAny<User>(), It.IsAny<User>(), manager, manager,
                It.IsAny<DateTime>(), It.IsAny<DateTime>(), null);
            var director = new User(0, "TestName", firm, "test@email.com", "0123456789", UsersEnum.Director);

            mock.Setup(c => c.Sign(contr, director)).Throws(new TestException());
            Action act = () => ctrl.Sign(contr, director);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Sign(contr, director), Times.Once);
        }
    }
}
