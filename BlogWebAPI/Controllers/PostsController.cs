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
        [AllowAnonymous]
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

        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreatePost([FromBody]PostsModel postModel)
        {
            try
            {
                var currentUser = new UsersController();
                using (var blogdb = new blogdbEntities())
                {
                    int lastPostId = (!blogdb.posts.Any()) ? 0 : blogdb.posts.OrderByDescending(p => p.id).First().id;
                    int nextPostId = lastPostId + 1;

                    posts newPost = new posts
                    {
                        id = nextPostId,
                        id_author = currentUser.GetLoginUserId(),
                        content = postModel.content
                    };

                    blogdb.Entry(newPost).State = EntityState.Added;
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "The Post was created successfully.");

                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);

            }
        }
        [Authorize]
        [HttpPut]
        public HttpResponseMessage EditPost(int id, [FromBody]PostsModel postModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    posts newPost = (from p in blogdb.posts
                                     where p.id == id
                                     select p).SingleOrDefault();
                    if (newPost != null)
                    {
                        newPost.id_author = postModel.idAuthor;
                        newPost.content = postModel.content;

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeletePost(int id)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    posts postToDelete = (from p in blogdb.posts
                                          where p.id == id
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}