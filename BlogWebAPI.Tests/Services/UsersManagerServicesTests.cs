using BlogDBSQLServer.Models;
using BlogWebAPI.Models.Users;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;

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
        public void Create_ValidUserData_ReturnsHttpStausOK()
        {
            var user = new User()
            {
                Username = "test",
                Password = "test231",
                Email = "test@gmail.com"
            };

            var result = _service.Create(user);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Delete_ValidUserId_ReturnsHttpStausOK()
        {
            int id = 4;
            _mockRepository.Setup(u => u.Delete(id)).Returns(true);

            var result = _service.Delete(id);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
        }

        [TestMethod()]
        public void Delete_InvalidUserId_ReturnsHttpStausNotFound()
        {
            int id = 25;
            _mockRepository.Setup(u => u.Delete(id)).Returns(false);

            var result = _service.Delete(id);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

        }

        [TestMethod()]
        public void Edit_ValidUserIdAndData_ReturnsHttpStatusOK()
        {
            int id = 4;
            var user = new User()
            {
                Username = "testUpdated",
                Password = "test231_1",
                Email = "testU@gmail.com"
            };
            _mockRepository.Setup(u => u.Edit(id, user)).Returns(new users());

            var result = _service.Edit(id,user);

            Assert.AreEqual(HttpStatusCode.OK,result.StatusCode);
        }

        [TestMethod()]
        public void Edit_InvalidUserIdAndValidUserData_ReturnsHttpStatusNotFound()
        {
            int id = 12;
            var user = new User()
            {
                Username = "testUpdated",
                Password = "test231_1",
                Email = "testU@gmail.com"
            };
            _mockRepository.Setup(u => u.Edit(id, user)).Returns((users)null);

            var result = _service.Edit(id, user);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void GetAllUsers_ReturnsANotNullList()
        {
            _mockRepository.Setup(u => u.GetAllUsers()).
                Returns(new List<users>()
                {
                    new users(){Id=1,Username="pepe",Password="test123", Email="test@gmail.com"},
                    new users(){Id=2,Username="pepe2",Password="test456", Email="test2@gmail.com"}
                });


            var result = _service.GetAllUsers();
            
            Assert.IsFalse(result.IsNullOrEmpty());
        }
    }
}