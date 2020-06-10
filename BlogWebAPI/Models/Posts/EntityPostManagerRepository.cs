using BlogDBSQLServer.Models;
using BlogWebAPI.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Posts
{
    public class EntityPostManagerRepository : IPostManagerRepository
    {
        readonly dbBlogEntities blogdb = new dbBlogEntities();
        readonly int loginUserId = new UsersManagerServices().GetLoginUserId();

        public posts Create(Post postToCreate)
        {
            var post = new posts
            {
                AuthorId = loginUserId,
                Content = postToCreate.Content
            };

            blogdb.Entry(post).State = EntityState.Added;
            blogdb.SaveChanges();

            return post;
        }

        public bool Delete(int postId)
        {
            var post = (from p in blogdb.posts
                        where p.Id == postId && p.AuthorId == loginUserId
                        select p).SingleOrDefault();
            
            if (post != null)
            {
                blogdb.Entry(post).State = EntityState.Deleted;
                blogdb.SaveChanges();
                return true;
            }
            return false;
        }

        public posts Edit(int postId, Post postToEdit)
        {
            var post = (from p in blogdb.posts
                        where p.Id == postId && p.AuthorId == loginUserId
                        select p).SingleOrDefault();
            
            if (post != null)
            {
                post.Content = postToEdit.Content;

                blogdb.Entry(post).State = EntityState.Modified;
                blogdb.SaveChanges();
            }
            return post;
        }

        public IEnumerable<object> GetPostsByUser(int userId)
        {
            List<posts> postsByUser = (from p in blogdb.posts
                                       where p.AuthorId == userId
                                       select p).ToList();
            var formattedPostsByUser = new List<object>();

            foreach (var post in postsByUser)
            {
                formattedPostsByUser.Add(new { post.Id, post.Content });
            }
            return formattedPostsByUser;
        }
    }
}