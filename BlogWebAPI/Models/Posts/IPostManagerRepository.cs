using BlogDBSQLServer.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Posts
{
    public interface IPostManagerRepository
    {
        posts Create(Post postToCreate);
        posts Edit(int postId, Post postToEdit);
        bool Delete(int postId);
        IEnumerable<object> GetPostsByUser(int userId);
    }
}
