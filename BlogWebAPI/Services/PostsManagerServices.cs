using BlogWebAPI.Models.Posts;
using System;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public class PostsManagerServices : IPostsManagerServices
    {
        readonly IPostManagerRepository repository;
        ErrorLogManager LogManager;
        public PostsManagerServices() : this(new EntityPostManagerRepository())
        { }
        public PostsManagerServices(IPostManagerRepository repository)
        {
            this.repository = repository;
        }
        public bool Create(PostsModel postToCreate)
        {
            try
            {
                repository.Create(postToCreate);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if(!repository.Delete(id))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public bool Edit(int id, PostsModel postToEdit)
        {
            try
            {
                repository.Edit(id, postToEdit);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public IEnumerable<object> ListOfPostsByUser(int id)
        {
            try
            {
                return repository.ListOfPostsByUser(id);
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return null;
            }
        }
    }
}