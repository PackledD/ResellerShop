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
    public class UserControllerTests
    {
        [TestMethod]
        public void UserCreateValid()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();

            ctrl.Create(user);
            mock.Verify(c => c.Add(user), Times.Once);
        }

        [TestMethod]
        public void UserCreateException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = new User(0, "TestUser", It.IsAny<Firm>(), "test@email.com", "0123456789", UsersEnum.Admin);

            mock.Setup(c => c.Add(user)).Throws(new TestException());
            Action act = () => ctrl.Create(user);
            Assert.ThrowsException<CreationException>(act);
            mock.Verify(c => c.Add(user), Times.Once);
        }

        [TestMethod]
        public void UserGetValid()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();
            int id = 0;

            mock.Setup(c => c.Get(id)).Returns(user);
            var res = ctrl.Get(id);
            mock.Verify(c => c.Get(id), Times.Once);
            Assert.AreEqual(user, res);
        }

        [TestMethod]
        public void UserGetException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();
            int id = 0;

            mock.Setup(c => c.Get(id)).Throws(new TestException());
            Action act = () => ctrl.Get(id);
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.Get(id), Times.Once);
        }

        [TestMethod]
        public void UserGetAllValid()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = new List<User>
            {
                It.IsAny<User>(),
                It.IsAny<User>(),
                It.IsAny<User>()
            };

            mock.Setup(c => c.GetAll()).Returns(user);
            var res = ctrl.GetAll();
            mock.Verify(c => c.GetAll(), Times.Once);
            Assert.AreEqual(user, res);
        }

        [TestMethod]
        public void UserGetAllException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);

            mock.Setup(c => c.GetAll()).Throws(new TestException());
            Action act = () => ctrl.GetAll();
            Assert.ThrowsException<GettingException>(act);
            mock.Verify(c => c.GetAll(), Times.Once);
        }

        [TestMethod]
        public void UserDeleteValid()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();
            int id = 0;

            ctrl.Delete(id);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void UserDeleteException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = new User(0, "TestUser", It.IsAny<Firm>(), "test@email.com", "0123456789", UsersEnum.Admin);
            int id = 0;

            mock.Setup(c => c.Delete(id)).Throws(new TestException());
            Action act = () => ctrl.Delete(id);
            Assert.ThrowsException<DeletingException>(act);
            mock.Verify(c => c.Delete(id), Times.Once);
        }

        [TestMethod]
        public void UserUpdateValid()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();

            ctrl.Update(user);
            mock.Verify(c => c.Update(user), Times.Once);
        }

        [TestMethod]
        public void UserUpdateException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = new User(0, "TestUser", It.IsAny<Firm>(), "test@email.com", "0123456789", UsersEnum.Admin);

            mock.Setup(c => c.Update(user)).Throws(new TestException());
            Action act = () => ctrl.Update(user);
            Assert.ThrowsException<UpdatingException>(act);
            mock.Verify(c => c.Update(user), Times.Once);
        }

        [TestMethod]
        public void UserAuthValidRight()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();
            int id = 0;
            string login = "login", password = "password";
            mock.Setup(c => c.Auth(login, password)).Returns(user);
            var res = ctrl.Auth(login, password);
            mock.Verify(c => c.Auth(login, password), Times.Once);
        }

        [TestMethod]
        public void UserAuthValidWrong()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            var user = It.IsAny<User>();
            string login = "login", password = "password";

            mock.Setup(c => c.Auth(login, password)).Returns(user);
            var res = ctrl.Auth(login, password);
            mock.Verify(c => c.Auth(login, password), Times.Once);
        }

        [TestMethod]
        public void UserAuthException()
        {
            var mock = new Mock<IUserRepository>();
            var ctrl = new UserController(mock.Object);
            string login = "login", password = "password";

            mock.Setup(c => c.Auth(login, password)).Throws(new TestException());
            Action act = () => ctrl.Auth(login, password);
            Assert.ThrowsException<AuthException>(act);
            mock.Verify(c => c.Auth(login, password), Times.Once);
        }
    }
}
