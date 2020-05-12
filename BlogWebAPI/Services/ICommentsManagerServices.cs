using BlogWebAPI.Models.Comments;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface ICommentsManagerServices
    {
        bool Create(int id,CommentsModel commentToCreate);
        bool Edit(int id, int id2, CommentsModel commentToEdit);
        bool Delete(int id, int id2);
        IEnumerable<object> ListOfPostsAndComments(int id);
    }
}
