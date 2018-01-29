using Natale.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Natale.Tests
{
    [TestClass]
    public class IDataBaseTest
    {



        #region testUpdate
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_AmountToy_Should_Throw_Exception_When_Toy_Is_Null()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.UpdateAmountToy(It.Is<Toy>(toy => toy == null))).Throws<ArgumentException>();

            mock.Object.UpdateAmountToy(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_AmountToy_Should_Return_True()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            bool result = mock.Object.UpdateAmountToy(new Toy());

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_UpdateStatus_Should_Throw_Exception_When_RequestKid_Is_Null()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.UpdateStatus(It.Is<RequestKid>(requestkid => requestkid == null))).Throws<ArgumentException>();

            mock.Object.UpdateAmountToy(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_UpdateStatus_Should_Return_True()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            bool result = mock.Object.UpdateStatus(new RequestKid());

            Assert.IsTrue(result);
        }
        #endregion



        #region GetAll
        [TestMethod]
        public void GetAllRequestKid_Should_Return_An_IEnumerable_Of_RequestKid()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetAllRequestKid()).Returns(new List<RequestKid>());

            var results = mock.Object.GetAllRequestKid();

            Assert.IsInstanceOfType(results, typeof(IEnumerable<RequestKid>));
        }

        [TestMethod]
        public void GetAllToys_Should_Return_An_IEnumerable_Of_Toy()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetAllToys()).Returns(new List<Toy>());

            var results = mock.Object.GetAllToys();

            Assert.IsInstanceOfType(results, typeof(IEnumerable<Toy>));
        }
        #endregion

        #region GetObject
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRequest_Should_Throw_Exception_When_id_Is_Null()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetRequest(It.Is<string>(id => id == null))).Throws<ArgumentNullException>();

            mock.Object.GetRequest(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRequest_Should_Throw_Exception_When_id_Is_Empty()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetRequest(It.Is<string>(id => id == string.Empty))).Throws<ArgumentException>();

            mock.Object.GetRequest(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRequest_Should_Throw_Exception_When_id_Is_Whitespaced()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetRequest(It.Is<string>(id => string.IsNullOrWhiteSpace(id) == true))).Throws<ArgumentException>();

            mock.Object.GetRequest("  ");
        }

        [TestMethod]
        public void GetRequest_Should_Return_An_Object_Of_Type_RequestKid()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetRequest(It.IsAny<string>())).Returns(new RequestKid());

            RequestKid result = mock.Object.GetRequest("test");

            Assert.IsInstanceOfType(result, typeof(RequestKid));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetToy_Should_Throw_Exception_When_id_Is_Null()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetToy(It.Is<string>(id => id == null))).Throws<ArgumentNullException>();

            mock.Object.GetToy(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetToy_Should_Throw_Exception_When_id_Is_Empty()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetToy(It.Is<string>(id => id == string.Empty))).Throws<ArgumentException>();

            mock.Object.GetToy(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetToy_Should_Throw_Exception_When_id_Is_Whitespaced()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetToy(It.Is<string>(id => string.IsNullOrWhiteSpace(id) == true))).Throws<ArgumentException>();

            mock.Object.GetToy("  ");
        }

        [TestMethod]
        public void GetToy_Should_Return_An_Object_Of_Type_Toy()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetToy(It.IsAny<string>())).Returns(new Toy());

            Toy result = mock.Object.GetToy("test");

            Assert.IsInstanceOfType(result, typeof(Toy));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUser_Should_Throw_Exception_When_user_Is_Null()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetUser(It.Is<User>(user => user == null))).Throws<ArgumentNullException>();

            mock.Object.GetUser(null);
        }

        [TestMethod]
        public void GetUser_Should_Return_An_Object_Of_Type_User()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.GetUser(It.IsAny<User>())).Returns(new User());

            User result = mock.Object.GetUser(new User());

            Assert.IsInstanceOfType(result, typeof(User));
        } 
        #endregion
    }
}