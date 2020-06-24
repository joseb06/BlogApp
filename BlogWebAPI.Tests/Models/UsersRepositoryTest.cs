using BlogDBSQLServer.Models;
using BlogWebAPI.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BlogWebAPI.Tests.Models
{
    [TestClass]
    public class UsersRepositoryTest
    {
        List<users> sampleDB;
        private Mock<IUserManagerRepository> _mockRepository;

        [TestInitialize()]
        public void Initialize()
        {
            sampleDB = GetUsers();
            _mockRepository = new Mock<IUserManagerRepository>();

            _mockRepository.Setup(m => m.Create(It.IsAny<User>())).Returns((User userToCreate) =>
            {
                 var us = new users()
                {
                    Id = sampleDB.Count + 1,
                    Username = userToCreate.Username,
                    Password = userToCreate.Password,
                    Email = userToCreate.Email
                };

                sampleDB.Add(us);
                return us;
            });

            _mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns((int id) =>
            {
                var user = sampleDB.Where(u => u.Id == id).FirstOrDefault();
                if (user == null)
                    return false;
                sampleDB.Remove(user);
                return true;
            });

            _mockRepository.Setup(m => m.Edit(It.IsAny<int>(), It.IsAny<User>())).Returns((int userId, User userToEdit) =>
            {
                var user = sampleDB.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.Username = userToEdit.Username;
                    user.Password = userToEdit.Password;
                    user.Email = userToEdit.Email;
                }
                return user;
            });

            _mockRepository.Setup(m => m.GetAllUsers()).Returns(sampleDB);
        }

        [TestMethod()]
        public void Create_ValidUserData_ReturnsNewUserAdded()
        {
            var user = new User()
            {
                Username = "test",
                Password = "test231",
                Email = "test@gmail.com"
            };

            var result = _mockRepository.Object.Create(user);
            int userQuantity = _mockRepository.Object.GetAllUsers().ToArray().Length;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, userQuantity);
        }

        [TestMethod()]
        public void Delete_ValidUserId_ReturnsTrue()
        {
            int id = 2;

            var result = _mockRepository.Object.Delete(id);
            int userQuantity = _mockRepository.Object.GetAllUsers().ToArray().Length;

            Assert.IsTrue(result);
            Assert.AreEqual(1, userQuantity);
        }

        [TestMethod()]
        public void Edit_ValidUserData_ReturnsUserUpdated()
        {
            int id = 1;

            var user = new User()
            {
                Username = "userModified",
                Password = "user123",
                Email = "user@gmail.com"
            };

            var result = _mockRepository.Object.Edit(id, user);

            Assert.IsNotNull(result);
            Assert.AreEqual("userModified", result.Username);
        }

        [TestMethod()]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            var result = _mockRepository.Object.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(sampleDB.Count, result.ToArray().Length);
        }


        private static List<users> GetUsers()
        {
            return new List<users>()
            {
                new users()
                {
                    Id=1,
                    Username = "user1",
                    Password = "test231",
                    Email = "test@gmail.com"
                },
                new users()
                {
                    Id=2,
                    Username = "user2",
                    Password = "test23145",
                    Email = "test1@gmail.com"
                }
            };
        }
    }
}
