using BlogWebAPI.Models.Posts;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface IPostsManagerServices
    {
        bool Create(PostsModel postToCreate);
        bool Edit(int id, PostsModel postToEdit);
        bool Delete(int id);
        IEnumerable<object> ListOfPostsByUser(int id);
    }
}
