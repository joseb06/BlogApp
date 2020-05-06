using BlogDatabase.Models;
using BlogWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        /// <summary>
        /// Method to get the list of all posts added by a specific user,
        /// also the comments of that post
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("BlogApp/Posts/{id}")]
        [HttpGet]
        public IEnumerable<object> GetListOfPostsAndHisComments(int id)
        {
            using (var blogdb = new blogdbEntities())
            {
                var postAndHisComments = from p in blogdb.posts
                                         where p.id == id
                                         select new
                                         {
                                             idPost = p.id,
                                             p.content,
                                             comments = (from c in blogdb.comments
                                                         where c.id_post == id
                                                         select new 
                                                         { 
                                                            userId = c.id_commentarist,
                                                            c.comment
                                                         }).ToList()
                                         };

                return postAndHisComments.ToArray();
            }
        }

        /// <summary>
        /// Method to add a new comment to a post
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <param name="commentsModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddComment(int id, [FromBody]CommentsModel commentsModel)
        {
            try
            {
                int loginUserId = new UsersController().GetLoginUserId();
                using (var blogdb = new blogdbEntities())
                {
                    int lastCommentId = (!blogdb.comments.Any()) ? 0 : blogdb.comments.OrderByDescending(p => p.id).First().id;
                    int nextCommentId = lastCommentId + 1;

                    comments newComment = new comments
                    {
                        id = nextCommentId,
                        id_post = id,
                        id_commentarist = loginUserId,
                        comment = commentsModel.Comment
                    };

                    blogdb.Entry(newComment).State = EntityState.Added;
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Your comment was added successfully.");

                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The comment could not be added",
                                                        MessageDetail = string.Format(e.Message)
                                                    });

            }
        }

        /// <summary>
        /// Method to modify the data in your own comment
        /// </summary>
        /// <param name="id">Identificator of the post</param>
        /// <param name="id2">Identificator of the comment</param>
        /// <param name="commentModel"></param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Posts/{id}/Comments/{id2}")]
        [HttpPut]
        public HttpResponseMessage EditComment(int id, int id2, [FromBody]CommentsModel commentModel)
        {
            try
            {
                int loginUserId = new UsersController().GetLoginUserId();
                using (var blogdb = new blogdbEntities())
                {
                    comments newComment = (from c in blogdb.comments
                                          where c.id_post == id && c.id == id2 && c.id_commentarist == loginUserId
                                          select c).SingleOrDefault();
                    if (newComment != null)
                    {
                        newComment.comment = commentModel.Comment;

                        blogdb.Entry(newComment).State = EntityState.Modified;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Your Comment was updated successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "The comment was not found to update");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The comment could not be updated",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }

        /// <summary>
        /// Method to remove your own comment from a post
        /// </summary>
        /// <param name="id">Identificator of the post</param>
        /// <param name="id2">Identificator of the comment</param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Posts/{id}/Comments/{id2}")]
        [HttpDelete]
        public HttpResponseMessage DeleteComment(int id, int id2)
        {
            try
            {
                int loginUserId = new UsersController().GetLoginUserId();
                using (var blogdb = new blogdbEntities())
                {
                    comments commentToDelete = (from c in blogdb.comments
                                          where c.id == id2 && c.id_post == id && c.id_commentarist == loginUserId
                                          select c).SingleOrDefault();
                    if (commentToDelete != null)
                    {
                        blogdb.Entry(commentToDelete).State = EntityState.Deleted;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK,
                            "The comment was deleted successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "The comment was not found to delete");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The comment could not be deleted",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }
    }
}
