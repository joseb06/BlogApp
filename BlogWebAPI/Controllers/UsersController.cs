using BlogDatabase.Models;
using BlogWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [NonAction]
        public int GetLoginUserId()
        {
            var loggedUserData = System.Security.Claims.ClaimsPrincipal.Current;
            return int.Parse(loggedUserData.FindFirst("userID").Value);
        }

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

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]UsersModel userModel)
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
                        userName = userModel.UserName,
                        password = userModel.Password,
                        email = userModel.Email
                    };

                    blogdb.Entry(newUser).State = System.Data.Entity.EntityState.Added;
                    blogdb.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "User was created successfully.");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "The User was not created",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
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
                        newUser.userName = userModel.UserName;
                        newUser.password = userModel.Password;
                        newUser.email = userModel.Email;

                        blogdb.Entry(newUser).State = System.Data.Entity.EntityState.Modified;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "User data was updated successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "User with Id: " + id.ToString() + " not found to update");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "User data cannot be updated",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
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
                        blogdb.Entry(newUser).State = System.Data.Entity.EntityState.Deleted;
                        blogdb.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK,
                            "The User data was deleted successfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "User with Id " + id.ToString() + " not found to delete");
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    new HttpError
                                                    {
                                                        Message = "User data cannot be deleted",
                                                        MessageDetail = string.Format(e.Message)
                                                    });
            }
        }
    }
}
