using BlogDatabase.Models;
using BlogWebAPI.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace BlogWebAPI.Controllers
{
    public class CommentsController : ApiController
    {

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
                        comment = commentsModel.comment
                    };

                    blogdb.Entry(newComment).State = EntityState.Added;
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Your comment was added successfully.");

                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);

            }
        }
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
                        newComment.comment = commentModel.comment;

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
