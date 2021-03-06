﻿using BlogDatabase.Models;
using System;
using System.Linq;

namespace BlogWebAPI.Models
{
    public class UserAuthentication : IDisposable
    {
        readonly blogdbEntities blogdb = new blogdbEntities();
        public users AuthenticateUser(string username, string password)
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