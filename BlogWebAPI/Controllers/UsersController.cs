using BlogWebAPI.Models.Users;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserManagerService service;

        public UsersController()
        {
            this.service = new UsersManagerServices();
        }

        public UsersController(IUserManagerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Method to create new users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]UsersModel user)
        {
            if (!service.Create(user))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The User was not created");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "User was created successfully.");
        }

        /// <summary>
        /// Method to modify the data of a registered user
        /// </summary>
        /// <param name="id">Identificator of the user's account</param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public HttpResponseMessage EditUser(int id, [FromBody]UsersModel user)
        {
            if (!service.Edit(id, user))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User data cannot be updated");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "User data was updated successfully.");
        }

        /// <summary>
        /// Method to cancel and delete the data of a registered user
        /// </summary>
        /// <param name="id">Identificator of the user's account</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            if (!service.Delete(id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User cannot be removed");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "User was removed successfully.");
        }

        /// <summary>
        /// Method to get the list of all registered users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public HttpResponseMessage ListOfUsers()
        {
            if (service.ListOfUsers() == null || !service.ListOfUsers().GetEnumerator().MoveNext())
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot display the list of Users");
            }

            return Request.CreateResponse(HttpStatusCode.OK, service.ListOfUsers());
        }
    }
}
