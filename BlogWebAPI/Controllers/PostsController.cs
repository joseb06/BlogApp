using BlogWebAPI.Models.Posts;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostsManagerServices _services;
        //public PostsController()
        //{
        //    this.services = new PostsManagerServices();
        //}
        public PostsController(IPostsManagerServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Method to create new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreatePost([FromBody]Post post)
        {
            var response = _services.Create(post);
            return Request.CreateResponse(response.StatusCode, response.Message);
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
            var response = _services.Delete(id);
            return Request.CreateResponse(response.StatusCode, response.Message);
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
        public HttpResponseMessage EditPost(int id, [FromBody]Post post)
        {
            var response = _services.Edit(id, post);
            return Request.CreateResponse(response.StatusCode, response.Message);
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
            var post = _services.GetPostsByUser(id);
            if (!post.GetEnumerator().MoveNext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ATTENTION! The list is empty or the post does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, post);
        }
    }
}