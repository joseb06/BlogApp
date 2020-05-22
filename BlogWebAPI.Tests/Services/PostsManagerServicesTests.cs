using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BlogWebAPI.Models.Posts;

namespace BlogWebAPI.Services.Tests
{
    [TestClass()]
    public class PostsManagerServicesTests
    {
        private Mock<IPostManagerRepository> _mockRepositoryPosts;
        private IPostsManagerServices _service;

        [TestInitialize()]
        public void Initialize()
        {
            _mockRepositoryPosts = new Mock<IPostManagerRepository>();
            _service = new PostsManagerServices(_mockRepositoryPosts.Object);
        }

        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            var post = new PostsModel
            {
                Content = "This is a test of a post"
            };

            //Act
            var result = _service.Create(post);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //Arrange
            int id = 1;

            //Act
            var result = _service.Delete(id);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Arrange
            int id = 2;
            var post = new PostsModel
            {
                Content = "This is a test of a updated post"
            };

            //Act
            var result = _service.Edit(id,post);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ListOfPostsByUserTest()
        {
            //Arrange
            int idAuthor=5;
            //Act
            var result = _service.ListOfPostsByUser(idAuthor);
            //Assert
            Assert.IsNotNull(result);
        }
    }
}