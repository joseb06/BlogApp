using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BlogDatabase.Models;
using BlogWebAPI.Models;

namespace BlogWebAPI.Controllers
{
    public class PostsController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<posts> GetListOfPosts()
        {
            using (var blogdb = new blogdbEntities())
            {
                var listAllPosts = blogdb.posts.ToList();
                return listAllPosts;
            }
        }
        //[Authorize]
        [HttpPost]
        public HttpResponseMessage CreatePost(PostsModel postModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    int lastPostId = blogdb.posts.OrderByDescending(p => p.id).First().id;
                    int nextPostId = lastPostId + 1;
                    posts newPost = new posts
                    {
                        id = nextPostId,
                        id_author = postModel.id_author,
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
                        newPost.id_author = postModel.id_author;
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