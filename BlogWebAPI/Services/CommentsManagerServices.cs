using BlogWebAPI.Models.Comments;
using System;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public class CommentsManagerServices : ICommentsManagerServices
    {
        readonly ICommentsManagerRepository repository;
        ErrorLogManager LogManager;
        //public CommentsManagerServices(): this(new EntityCommentsManangerRepository())
        //{
                
        //}
        public CommentsManagerServices(ICommentsManagerRepository repository)
        {
            this.repository = repository;
        }
        public bool Create(int id, CommentsModel commentToCreate)
        {
            try
            {
                repository.Create(id, commentToCreate);
                return true;
            }
            catch(Exception ex) 
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public bool Delete(int id, int id2)
        {
            try
            {
                repository.Delete(id, id2);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public bool Edit(int id, int id2, CommentsModel commentToEdit)
        {
            try
            {
                repository.Edit(id, id2, commentToEdit);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public IEnumerable<object> ListOfPostsAndComments(int id)
        {
            try
            {
                return repository.ListOfPostsAndComments(id);
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return null;
            }
        }
    }
}