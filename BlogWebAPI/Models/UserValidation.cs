using BlogDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWebAPI.Models
{
    public class UserValidation : IDisposable
    {
        readonly blogdbEntities blogdb = new blogdbEntities();
        public users ValidateUser(string username, string password)
        {
            return blogdb.users.FirstOrDefault(u => u.userName == username &&
                                                    u.password == password);
        }
        public void Dispose()
        {
            blogdb.Dispose();
        }
    }
}