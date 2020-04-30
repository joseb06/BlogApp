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
        readonly blogdbEntities blogdb = new blogdbEntities();
        
        [Authorize]
        [HttpGet]
        public IEnumerable<users> GetListOfUsers()
        {
            var listAllUsers = blogdb.users.ToList();
            return listAllUsers;

        }
    }
}
