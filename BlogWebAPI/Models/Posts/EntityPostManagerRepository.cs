using BlogDatabase.Models;
using BlogWebAPI.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Posts
{
    public class EntityPostManagerRepository : IPostManagerRepository
    {
        readonly blogdbEntities blogdb = new blogdbEntities();
        readonly int loginUserId = new UsersManagerServices().GetLoginUserId();

        public posts Create(PostsModel postToCreate)
        {
            int lastPostId = (!blogdb.posts.Any()) ? 0 : blogdb.posts.OrderByDescending(p => p.id).First().id;
            int nextPostId = lastPostId + 1;

            posts newPost = new posts
            {
                id = nextPostId,
                id_author = loginUserId,
                content = postToCreate.Content
            };

            blogdb.Entry(newPost).State = EntityState.Added;
            blogdb.SaveChanges();

            return newPost;
        }

        public bool Delete(int id)
        {
            posts postToDelete = (from p in blogdb.posts
                                  where p.id == id && p.id_author == loginUserId
                                  select p).SingleOrDefault();
            if (postToDelete == null)
                return false;

            blogdb.Entry(postToDelete).State = EntityState.Deleted;
            blogdb.SaveChanges();

            return true;
        }

        public posts Edit(int id, PostsModel postToEdit)
        {
            posts newPost = (from p in blogdb.posts
                             where p.id == id && p.id_author == loginUserId
                             select p).SingleOrDefault();

            newPost.content = postToEdit.Content;

            blogdb.Entry(newPost).State = EntityState.Modified;
            blogdb.SaveChanges();

            return newPost;
        }

        public IEnumerable<object> ListOfPostsByUser(int id)
        {
            List<posts> userPost = (from p in blogdb.posts
                                    where p.id_author == id
                                    select p).ToList();
            List<object> listOfPosts = new List<object>();

            foreach (var item in userPost)
            {
                listOfPosts.Add(new { item.id, item.content });
            }

            return listOfPosts;
        }
    }
}