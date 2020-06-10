using BlogDBSQLServer.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Posts
{
    public interface IPostManagerRepository
    {
        posts Create(Post userToCreate);
        posts Edit(int postId, Post userToEdit);
        bool Delete(int postId);
        IEnumerable<object> GetPostsByUser(int userId);
    }
}
