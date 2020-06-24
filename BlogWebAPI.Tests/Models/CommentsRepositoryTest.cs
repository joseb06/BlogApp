using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogDBSQLServer.Models;
using Moq;
using BlogWebAPI.Models.Comments;
using System.Linq;

namespace BlogWebAPI.Tests.Models
{
    /// <summary>
    /// Summary description for CommentsRepositoryTest
    /// </summary>
    [TestClass]
    public class CommentsRepositoryTest
    {
        List<comments> sampleDB;
        private Mock<ICommentsManagerRepository> _mockRepository;
        int loginUserID;
        int postId;

        [TestInitialize()]
        public void Initialize()
        {
            sampleDB = GetComments();
            _mockRepository = new Mock<ICommentsManagerRepository>();
            loginUserID = 2;
            postId = 1;

            _mockRepository.Setup(m => m.Create(It.IsAny<int>(),It.IsAny<Comment>())).Returns((int id, Comment commentToCreate) =>
            {
                var comment = new comments()
                {
                    Id = sampleDB.Count + 1,
                    PostId=id,
                    CommentaristId = loginUserID,
                    Comment = commentToCreate.Comments
                };

                sampleDB.Add(comment);
                return comment;
            });

            _mockRepository.Setup(m => m.Delete(It.IsAny<int>(), It.IsAny<int>())).Returns((int postId, int commentId) =>
            {
                var comment = sampleDB.Where(c => c.PostId == postId &&
                                           c.Id == commentId &&    
                                           c.CommentaristId == loginUserID).FirstOrDefault();
                
                if (comment == null)
                    return false;
                sampleDB.Remove(comment);
                return true;
            });

            _mockRepository.Setup(m => m.Edit(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Comment>())).Returns((int postId, int commentId, Comment commentToEdit) =>
            {
                var comment = sampleDB.Where(c => c.PostId == postId &&
                                           c.Id == commentId &&
                                           c.CommentaristId == loginUserID).FirstOrDefault();
                if (comment != null)
                {
                    comment.Comment = commentToEdit.Comments;
                }
                return comment;
            });

            _mockRepository.Setup(m => m.GetPostComments(postId)).Returns(sampleDB);
        }

        [TestMethod()]
        public void Create_ValidCommentData_ReturnsNewCommentAdded()
        {
            var comment = new Comment()
            {
                Comments = "This is a test of a comment in the post"
            };

            var result = _mockRepository.Object.Create(postId, comment);
            int commentsQuantity = _mockRepository.Object.GetPostComments(postId).ToArray().Length;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, commentsQuantity);
        }

        [TestMethod()]
        public void Delete_ValidPostAndCommentId_ReturnsTrue()
        {
            int id = 2;

            var result = _mockRepository.Object.Delete(postId, id);
            int commentsQuantity = _mockRepository.Object.GetPostComments(postId).ToArray().Length;

            Assert.IsTrue(result);
            Assert.AreEqual(1, commentsQuantity);
        }

        [TestMethod()]
        public void Edit_ValidCommentData_ReturnsCommentUpdated()
        {
            int id = 1;

            var comment = new Comment()
            {
                Comments = "This comment was modified"
            };

            var result = _mockRepository.Object.Edit(postId, id, comment);

            Assert.IsNotNull(result);
            Assert.AreEqual("This comment was modified", result.Comment);
        }

        [TestMethod()]
        public void GetPostComments_ReturnsListOfCommentsByPost()
        {
            var result = _mockRepository.Object.GetPostComments(postId);

            Assert.IsNotNull(result);
            Assert.AreEqual(sampleDB.Count, result.ToArray().Length);
        }

        private static List<comments> GetComments()
        {
            return new List<comments>()
            {
                new comments()
                {
                    Id=1,
                    PostId=1,
                    CommentaristId=2,
                    Comment="First Example of a post"
                },
                new comments()
                {
                    Id=2,
                    PostId=1,
                    CommentaristId=2,
                    Comment="Second Example of a post"
                }
            };
        }
    }
}
