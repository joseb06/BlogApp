using BlogWebAPI.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BlogWebAPI.Services.Tests
{
    [TestClass()]
    public class UsersManagerServicesTests
    {
        private Mock<IUserManagerRepository> _mockRepository;
        private IUserManagerService _service;

        [TestInitialize()]
        public void Initialize()
        {
            _mockRepository = new Mock<IUserManagerRepository>();
            _service = new UsersManagerServices(_mockRepository.Object);
        }

        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            var user = new UsersModel()
            {
                UserName = "test",
                Password = "test231",
                Email = "test@gmail.com"
            };

            //Act
            var result = _service.Create(user);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //Arrange
            int id = 102;
            //Act
            var result = _service.Delete(id);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void EditTest()
        {
            int idUser = 2;
            //Arrange
            var user = new UsersModel()
            {
                UserName = "testUpdated",
                Password = "test231_1",
                Email = "testU@gmail.com"
            };

            //Act
            var result = _service.Edit(idUser, user);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ListOfUsersTest()
        {
            //Arrange

            //Act
            var result = _service.ListOfUsers();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}