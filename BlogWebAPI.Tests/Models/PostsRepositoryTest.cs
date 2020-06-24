using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogDBSQLServer.Models;
using Moq;
using BlogWebAPI.Models.Posts;
using System.Linq;
using System.Collections.Generic;

namespace BlogWebAPI.Tests.Models
{
    /// <summary>
    /// Summary description for PostsRepositoryTest
    /// </summary>
    [TestClass]
    public class PostsRepositoryTest
    {
        List<posts> sampleDB;
        private Mock<IPostManagerRepository> _mockRepository;
        int loginUserID;

        [TestInitialize()]
        public void Initialize()
        {
            sampleDB = GetPosts();
            _mockRepository = new Mock<IPostManagerRepository>();
            loginUserID = 1;

            _mockRepository.Setup(m => m.Create(It.IsAny<Post>())).Returns((Post postToCreate) =>
            {
                var post = new posts()
                {
                    Id= sampleDB.Count + 1,
                    AuthorId = loginUserID,
                    Content = postToCreate.Content
                };

                sampleDB.Add(post);
                return post;
            });

            _mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns((int id) =>
            {
                var post = sampleDB.Where(u => u.Id == id).FirstOrDefault();
                if (post == null)
                    return false;
                sampleDB.Remove(post);
                return true;
            });

            _mockRepository.Setup(m => m.Edit(It.IsAny<int>(), It.IsAny<Post>())).Returns((int postId, Post postToEdit) =>
            {
                var post = sampleDB.Where(u => u.Id == postId).FirstOrDefault();
                if (post != null)
                {
                    post.Content = postToEdit.Content;
                }
                return post;
            });

            _mockRepository.Setup(m => m.GetPostsByUser(loginUserID)).Returns(sampleDB);
        }

        [TestMethod()]
        public void Create_ValidPostData_ReturnsNewPostAdded()
        {
            var post = new Post()
            {
                Content = "This is a test of a post"
            };

            var result = _mockRepository.Object.Create(post);
            int postsQuantity = _mockRepository.Object.GetPostsByUser(loginUserID).ToArray().Length;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, postsQuantity);
        }

        [TestMethod()]
        public void Delete_ValidpostId_ReturnsTrue()
        {
            int id = 2;

            var result = _mockRepository.Object.Delete(id);
            int postsQuantity = _mockRepository.Object.GetPostsByUser(loginUserID).ToArray().Length;

            Assert.IsTrue(result);
            Assert.AreEqual(1, postsQuantity);
        }

        [TestMethod()]
        public void Edit_ValidPostData_ReturnsPostUpdated()
        {
            int id = 1;

            var post = new Post()
            {
                 Content = "This post was modified"
            };

            var result = _mockRepository.Object.Edit(id, post);

            Assert.IsNotNull(result);
            Assert.AreEqual("This post was modified", result.Content);
        }

        [TestMethod()]
        public void GetPostByUser_ReturnsListOfPostByUser()
        {
            var result = _mockRepository.Object.GetPostsByUser(loginUserID);

            Assert.IsNotNull(result);
            Assert.AreEqual(sampleDB.Count, result.ToArray().Length);
        }

        private static List<posts> GetPosts()
        {
            return new List<posts>()
            {
                new posts()
                {
                    Id=1,
                    AuthorId=1,
                    Content="First Example of a post"
                },
                new posts()
                {
                    Id=2,
                    AuthorId=1,
                    Content="Second Example of a post"
                }
            };
        }
    }
}
