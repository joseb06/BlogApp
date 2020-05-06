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
    public class PostsController : ApiController
    {
        readonly int loginUserId = new UsersController().GetLoginUserId();

        /// <summary>
        /// Method to get the list of all posts added by a specific user
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Users/{id}/Posts")]
        [HttpGet]
        public IEnumerable<Object> GetListOfPostsByUser(int id)
        {
            using (var blogdb = new blogdbEntities())
            {
                List<posts> userPost = (from p in blogdb.posts
                                        where p.id_author == id
                                        select p).ToList();
                List<object> listOfPosts = new List<object>();

                foreach (var item in userPost)
                {
                    listOfPosts.Add(new { item.id, item.content });
                }
                return listOfPosts;
            }
        }

        /// <summary>
        /// Method to create new post
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreatePost([FromBody]PostsModel postModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    int lastPostId = (!blogdb.posts.Any()) ? 0 : blogdb.posts.OrderByDescending(p => p.id).First().id;
                    int nextPostId = lastPostId + 1;

                    posts newPost = new posts
                    {
                        id = nextPostId,
                        id_author = loginUserId,
                        content = postModel.Content
                    };

                    blogdb.Entry(newPost).State = EntityState.Added;
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "The Post was created successfully.");

                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The Post could not be created",
                                                        MessageDetail = string.Format(e.Message)
                                                    });

            }
        }

        /// <summary>
        /// Method to modify the data in your own publication
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public HttpResponseMessage EditPost(int id, [FromBody]PostsModel postModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    posts newPost = (from p in blogdb.posts
                                     where p.id == id && p.id_author == loginUserId
                                     select p).SingleOrDefault();
                    if (newPost != null)
                    {
                        newPost.content = postModel.Content;

                        blogdb.Entry(newPost).State = EntityState.Modified;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "The Post was updated successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "The Post with Id: " + id.ToString() + " not found to update");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The Post could not be updated",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }

        /// <summary>
        /// Method to delete your own publications
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeletePost(int id)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    posts postToDelete = (from p in blogdb.posts
                                          where p.id == id && p.id_author == loginUserId
                                          select p).SingleOrDefault();
                    if (postToDelete != null)
                    {
                        blogdb.Entry(postToDelete).State = EntityState.Deleted;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK,
                            "The Post was deleted successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "The Post with Id " + id.ToString() + " not found to delete");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The Post could not be deleted",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }
    }
}