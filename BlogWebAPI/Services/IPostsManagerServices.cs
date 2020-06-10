using BlogWebAPI.Models;
using BlogWebAPI.Models.Posts;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface IPostsManagerServices
    {
        ResponseModel Create(Post postToCreate);
        ResponseModel Edit(int postId, Post postToEdit);
        ResponseModel Delete(int postId);
        IEnumerable<object> GetPostsByUser(int userId);
    }
}
