using BlogWebAPI.Models.Comments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BlogWebAPI.Services.Tests
{
    [TestClass()]
    public class CommentsManagerServicesTests
    {
        private Mock<ICommentsManagerRepository> _mockRepositoryComments;
        private ICommentsManagerServices _commentsService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockRepositoryComments = new Mock<ICommentsManagerRepository>();
            _commentsService = new CommentsManagerServices(_mockRepositoryComments.Object);
        }

        [TestMethod()]
        public void CreateTest()
        {
            //Arrange
            int idPost = 5;
            var comment = new CommentsModel
            {
                Comment = "This a test of a comment in a post"
            };
            //Act
            var result = _commentsService.Create(idPost, comment);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //Arrange
            int idPost = 5;
            int idComment = 1;
            //Act
            var result = _commentsService.Delete(idPost, idComment);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Arrange
            int idPost = 5;
            int idComment = 1;
            var comment = new CommentsModel
            {
                Comment = "This a test of a updated comment in a post"
            };
            //Act
            var result = _commentsService.Edit(idPost, idComment, comment);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ListOfPostsAndCommentsTest()
        {
            //Arrange
            int idPost = 5;
            //Act
            var result = _commentsService.ListOfPostsAndComments(idPost);
            //Assert
            Assert.IsNotNull(result);
        }
    }
}