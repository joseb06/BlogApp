using BlogDBSQLServer.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Comments
{
    public interface ICommentsManagerRepository
    {
        comments Create(int postId, Comment commentToCreate);
        comments Edit(int postId, int commentId, Comment commentToEdit);
        bool Delete(int postId, int commentId);
        IEnumerable<object> GetPostComments(int postId);
    }
}
