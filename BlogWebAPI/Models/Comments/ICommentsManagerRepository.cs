using BlogDatabase.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Comments
{
    public interface ICommentsManagerRepository
    {
        comments Create(int id, CommentsModel commentToCreate);
        comments Edit(int id, int id2, CommentsModel commentToEdit);
        bool Delete(int id, int id2);
        IEnumerable<object> ListOfPostsAndComments(int id);
    }
}
