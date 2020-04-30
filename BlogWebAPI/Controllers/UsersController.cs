using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogDatabase.Models;
using BlogWebAPI.Models;

namespace BlogWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<users> GetListOfUsers()
        {
            using (var blogdb = new blogdbEntities())
            {
                var listAllUsers = blogdb.users.ToList();
                return listAllUsers;
            }
        }
        //[Authorize]
        [HttpPost]
        public HttpResponseMessage CreateUser(UsersModel userModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    users newUser = new users
                    {
                        id = userModel.id,
                        userName = userModel.userName,
                        password = userModel.password,
                        email = userModel.email
                    };

                    blogdb.users.Add(newUser);
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "User was created successfully.");
                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                 
            }
        }
    }
}
