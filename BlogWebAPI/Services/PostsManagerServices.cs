using BlogWebAPI.Models;
using BlogWebAPI.Models.Posts;
using System;
using System.Collections.Generic;
using System.Net;

namespace BlogWebAPI.Services
{
    public class PostsManagerServices : IPostsManagerServices
    {
        readonly IPostManagerRepository _repository;

        public PostsManagerServices(IPostManagerRepository repository)
        {
            _repository = repository;
        }
        public ResponseModel Create(Post postToCreate)
        {
            try
            {
                _repository.Create(postToCreate);
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Post was created successfully."
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Post cannot be created."
                };
            }
        }

        public ResponseModel Delete(int postId)
        {
            try
            {
                if (!_repository.Delete(postId))
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a Post with ID = {0}", postId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Post was removed successfully."
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Post cannot be removed."
                };
            }
        }

        public ResponseModel Edit(int postId, Post postToEdit)
        {
            try
            {
                if (_repository.Edit(postId, postToEdit) == null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a Post with ID = {0}", postId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Post was updated successfully"
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Post cannot be updated"
                };
            }
        }

        public IEnumerable<object> GetPostsByUser(int userId)
        {
            return _repository.GetPostsByUser(userId);
        }
    }
}