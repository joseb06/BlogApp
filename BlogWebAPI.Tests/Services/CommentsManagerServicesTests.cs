using BlogDBSQLServer.Models;
using BlogWebAPI.Models.Comments;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BlogWebAPI.Services.Tests
{
    [TestClass()]
    public class CommentsManagerServicesTests
    {
        private Mock<ICommentsManagerRepository> _mockRepository;
        private ICommentsManagerServices _service;

        [TestInitialize()]
        public void Initialize()
        {
            _mockRepository = new Mock<ICommentsManagerRepository>();
            _service = new CommentsManagerServices(_mockRepository.Object);
        }

        [TestMethod()]
        public void Create_ValidPostIdAndCommentData_ReturnsHttpStatusOK()
        {
            int id = 5;
            var comment = new Comment
            {
                Comments = "This a test of a comment in a post"
            };
            _mockRepository.Setup(c => c.Create(id, comment)).Returns(new comments());
            
            var result = _service.Create(id, comment);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Create_InvalidPostIdAndValidCommentData_ReturnsHttpStatusNotFound()
        {
            int id = 15;
            var comment = new Comment
            {
                Comments = "This a test of a comment in a post"
            };
            _mockRepository.Setup(c => c.Create(id, comment)).Returns((comments)null);

            var result = _service.Create(id, comment);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void Delete_ValidPostAndCommentId_ReturnsHttpStatusOK()
        {
            int postId = 5;
            int CommentId = 1;
            _mockRepository.Setup(c => c.Delete(postId, CommentId)).Returns(true);

            
            var result = _service.Delete(postId, CommentId);
            
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Delete_InvalidPostOrCommentId_ReturnsHttpStatusNotFound()
        {
            int postId = 51;
            int CommentId = 11;
            _mockRepository.Setup(c => c.Delete(postId, CommentId)).Returns(false);


            var result = _service.Delete(postId, CommentId);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void Edit_ValidPostAndCommentIdAndValidCommentData_ReturnsHttpStatusOK()
        {
            int postId = 5;
            int commentId = 1;
            var comment = new Comment
            {
                Comments = "This a test of a updated comment in a post"
            };
            _mockRepository.Setup(c => c.Edit(postId, commentId, comment)).Returns(new comments());
            
            var result = _service.Edit(postId, commentId, comment);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void Edit_InvalidPostOrCommentIdAndValidCommentData_ReturnsHttpStatusNotFound()
        {
            int postId = 33;
            int commentId = 4;
            var comment = new Comment
            {
                Comments = "This a test of a updated comment in a post"
            };
            _mockRepository.Setup(c => c.Edit(postId, commentId, comment)).Returns((comments)null);

            var result = _service.Edit(postId, commentId, comment);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod()]
        public void GetPostComments_ValidPostId_ReturnsANotNullList()
        {
            int id = 1;
            _mockRepository.Setup(c => c.GetPostComments(id)).
                Returns(new List<comments>()
                {
                    new comments() {Id=1,PostId=1,CommentaristId=2,Comment="this is a test" },
                    new comments() {Id=2,PostId=1,CommentaristId=2,Comment="this is a secondtest" }
                });

            var result = _service.GetPostComments(id);
            
            Assert.IsFalse(result.IsNullOrEmpty());
        }
    }
}