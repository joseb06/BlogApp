using BlogWebAPI.Models.Users;
using BlogWebAPI.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserManagerService _service;

        //public UsersController()
        //{
        //    this.service = new UsersManagerServices();
        //}

        public UsersController(IUserManagerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Method to create new users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]User user)
        {
            var response = _service.Create(user);
            return Request.CreateResponse(response.StatusCode, response.Message);
        }

        /// <summary>
        /// Method to modify the data of a registered user
        /// </summary>
        /// <param name="id">Identificator of the user's account</param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public HttpResponseMessage EditUser(int id, [FromBody]User user)
        {
            var response = _service.Edit(id, user);
            return Request.CreateResponse(response.StatusCode, response.Message);
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
            var response = _service.Delete(id);
            return Request.CreateResponse(response.StatusCode,response.Message);
        }

        /// <summary>
        /// Method to get the list of all registered users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public HttpResponseMessage ListOfUsers()
        {
            var users = _service.GetAllUsers();
            if (!users.GetEnumerator().MoveNext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ATTENTION! The list is empty");
            }
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }
    }
}
