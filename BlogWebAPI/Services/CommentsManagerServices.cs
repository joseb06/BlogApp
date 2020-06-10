using BlogWebAPI.Models;
using BlogWebAPI.Models.Comments;
using System;
using System.Collections.Generic;
using System.Net;

namespace BlogWebAPI.Services
{
    public class CommentsManagerServices : ICommentsManagerServices
    {
        readonly ICommentsManagerRepository _repository;

        public CommentsManagerServices(ICommentsManagerRepository repository)
        {
            _repository = repository;
        }
        public ResponseModel Create(int postId, Comment commentToCreate)
        {
            try
            {
                if (_repository.Create(postId, commentToCreate) != null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Comment was created successfully."
                    };
                }
                else
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("The post with the Id:{0} not exist to add a Comment.",postId)
                    };
                }

            }
            catch(Exception ex) 
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Comment cannot be created."
                };
            }
        }

        public ResponseModel Delete(int postId, int commentId)
        {
            try
            {
                if (!_repository.Delete(postId, commentId))
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a Comment with ID = {0}", commentId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Comment was removed successfully."
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Comment cannot be removed."
                };
            }
        }

        public ResponseModel Edit(int postId, int commentId, Comment commentToEdit)
        {
            try
            {
                if (_repository.Edit(postId, commentId, commentToEdit) == null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a Comment with ID = {0}", commentId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Comment was updated successfully"
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Comment cannot be updated"
                };
            }
        }

        public IEnumerable<object> GetPostComments(int postId)
        {
            return _repository.GetPostComments(postId);
        }
    }
}