using BlogWebAPI.Models.Comments;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentsManagerServices _services;
        //public CommentsController()
        //{
        //    this.services = new CommentsManagerServices();
        //}
        public CommentsController(ICommentsManagerServices services)
        {
            _services = services;
        }
        /// <summary>
        /// Method to add a new comment to a post
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddComment(int id, [FromBody]Comment comment)
        {
            var response = _services.Create(id, comment);
            return Request.CreateResponse(response.StatusCode, response.Message);
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
            var response = _services.Delete(id, id2);
            return Request.CreateResponse(response.StatusCode, response.Message);
        }

        /// <summary>
        /// Method to modify the data in your own comment
        /// </summary>
        /// <param name="id">Identificator of the post</param>
        /// <param name="id2">Identificator of the comment</param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [Route("BlogApp/Posts/{id}/Comments/{id2}")]
        [HttpPut]
        public HttpResponseMessage EditComment(int id, int id2, [FromBody]Comment comment)
        {
            var response = _services.Edit(id, id2, comment);
            return Request.CreateResponse(response.StatusCode, response.Message);
        }

        /// <summary>
        /// Method to get the list of all posts added by a specific user,
        /// also the comments of that post
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("BlogApp/Posts/{id}")]
        [HttpGet]
        public HttpResponseMessage GetListOfPostsAndHisComments(int id)
        {
            var comment = _services.GetPostComments(id);
            if (!comment.GetEnumerator().MoveNext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ATTENTION! The list is empty or the post does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, comment);
        }
    }
}
