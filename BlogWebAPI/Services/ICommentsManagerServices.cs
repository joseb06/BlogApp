using BlogWebAPI.Models;
using BlogWebAPI.Models.Comments;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface ICommentsManagerServices
    {
        ResponseModel Create(int postId,Comment commentToCreate);
        ResponseModel Edit(int postId, int commentId, Comment commentToEdit);
        ResponseModel Delete(int postId, int commentId);
        IEnumerable<object> GetPostComments(int postId);
    }
}
