using BlogDatabase.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Posts
{
    public interface IPostManagerRepository
    {
        posts Create(PostsModel userToCreate);
        posts Edit(int id, PostsModel userToEdit);
        bool Delete(int id);
        IEnumerable<object> ListOfPostsByUser(int id);
    }
}
