using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BlogWebAPI.Models.Posts;
using System.Net;
using BlogDBSQLServer.Models;
using System.Collections.Generic;
using Castle.Core.Internal;

namespace BlogWebAPI.Services.Tests
{
    [TestClass()]
    public class PostsManagerServicesTests
    {
        private Mock<IPostManagerRepository> _mockRepository;
        private IPostsManagerServices _service;

        [TestInitialize()]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostManagerRepository>();
            _service = new PostsManagerServices(_mockRepository.Object);
        }

        [TestMethod()]
        public void Create_ValidPostData_ReturnsHttpStatusOK()
        {
            var post = new Post
            {
                Content = "This is a test of a post"
            };

            var result = _service.Create(post);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Delete_ValidPostId_ReturnsHttpStatusOK()
        {
            int id = 1;
            _mockRepository.Setup(p => p.Delete(id)).Returns(true);

            var result = _service.Delete(id);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Delete_InvalidPostId_ReturnsHttpStatusNotFound()
        {
            int id = 16;
            _mockRepository.Setup(p => p.Delete(id)).Returns(false);

            var result = _service.Delete(id);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void Edit_ValidPostIdAndData_ReturnsHttpStatusOK()
        {
            int id = 2;
            var post = new Post
            {
                Content = "This is a test of a updated post"
            };
            _mockRepository.Setup(p => p.Edit(id, post)).Returns(new posts());

            var result = _service.Edit(id,post);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Edit_InvalidPostIdAndValidPostData_ReturnsHttpStatusNotFound()
        {
            int id = 25;
            var post = new Post
            {
                Content = "This is a test of a updated post"
            };
            _mockRepository.Setup(p => p.Edit(id, post)).Returns((posts)null);

            var result = _service.Edit(id, post);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void GetPostsByUser_ValidUserId_ReturnsANotNullList()
        {
            int id=1;
            _mockRepository.Setup(p => p.GetPostsByUser(id)).
                Returns(new List<object>()
                {
                    new { Id = 1, AuthorId = 1, Content = "first post" },
                    new { Id = 2, AuthorId = 1, Content = "second post" }
                });

            var result = _service.GetPostsByUser(id);
            
            Assert.IsFalse(result.IsNullOrEmpty());
        }
    }
}