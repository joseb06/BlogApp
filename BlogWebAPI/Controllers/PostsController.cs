using BlogWebAPI.Models.Posts;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class PostsController : ApiController
    {
        IPostsManagerServices services;
        //public PostsController()
        //{
        //    this.services = new PostsManagerServices();
        //}
        public PostsController(IPostsManagerServices services)
        {
            this.services = services;
        }

        /// <summary>
        /// Method to create new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreatePost([FromBody]PostsModel post)
        {
            if (!services.Create(post))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The Post could not be created");
            return Request.CreateResponse(HttpStatusCode.OK, "The Post was created successfully.");
        }

        /// <summary>
        /// Method to delete your own publications
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/PostD/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeletePost(int id)
        {
            if (!services.Delete(id))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The Post could not be deleted");
            return Request.CreateResponse(HttpStatusCode.OK, "The Post was deleted successfully.");
        }

        /// <summary>
        /// Method to modify the data in your own publication
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <param name="post"></param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Post/{id}")]
        [HttpPut]
        public HttpResponseMessage EditPost(int id, [FromBody]PostsModel post)
        {
            if (!services.Edit(id, post))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The Post could not be updated");
            return Request.CreateResponse(HttpStatusCode.OK, "The Post was updated successfully.");
        }

        /// <summary>
        /// Method to get the list of all posts added by a specific user
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Users/{id}/Posts")]
        [HttpGet]
        public HttpResponseMessage ListOfPostsByUser(int id)
        {
            var list = services.ListOfPostsByUser(id);

            if (!list.GetEnumerator().MoveNext() || list == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The Post could not be shown");
            return Request.CreateResponse(HttpStatusCode.OK, services.ListOfPostsByUser(id));
        }
    }
}