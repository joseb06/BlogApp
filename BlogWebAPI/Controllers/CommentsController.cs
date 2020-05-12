using BlogWebAPI.Models.Comments;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        ICommentsManagerServices services;
        public CommentsController()
        {
            this.services = new CommentsManagerServices();
        }
        public CommentsController(ICommentsManagerServices services)
        {
            this.services = services;
        }
        /// <summary>
        /// Method to add a new comment to a post
        /// </summary>
        /// <param name="id">Identificator of the user who created the post</param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddComment(int id, [FromBody]CommentsModel comment)
        {
            if (!services.Create(id,comment))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The comment could not be added");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Your comment was added successfully.");
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
            if (!services.Delete(id,id2))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The comment could not be deleted");
            }
            return Request.CreateResponse(HttpStatusCode.OK,"The comment was deleted successfully.");
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
        public HttpResponseMessage EditComment(int id, int id2, [FromBody]CommentsModel comment)
        {
            if (!services.Edit(id,id2,comment))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The comment could not be updated");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Your Comment was updated successfully.");
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
            var list = services.ListOfPostsAndComments(id);
            
            if(!list.GetEnumerator().MoveNext() || list == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The Post could not be shown");
            return Request.CreateResponse(HttpStatusCode.OK, services.ListOfPostsAndComments(id));
        }
    }
}
