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
                    int lastUserId = blogdb.users.OrderByDescending(u => u.id).First().id;
                    int nextUserId = lastUserId + 1;
                    users newUser = new users
                    {
                        id = nextUserId,
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
        [Authorize]
        [HttpPut]
        public HttpResponseMessage PutEditUser(int id, [FromBody]UsersModel userModel)
        {
            try
            {
                using (var blogdb = new blogdbEntities())
                {
                    users newUser = (from u in blogdb.users
                                     where u.id == id
                                     select u).SingleOrDefault();
                    if (newUser != null)
                    {
                        newUser.userName = userModel.userName;
                        newUser.password = userModel.password;
                        newUser.email = userModel.email;

                        blogdb.Entry(newUser).State = System.Data.Entity.EntityState.Modified;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "User data was updated successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "User with Id " + id.ToString() + " not found to update");
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
