using BlogDBSQLServer.Models;
using System;
using System.Linq;

namespace BlogWebAPI.Models.Users
{
    public class UserAuthentication : IDisposable
    {
        readonly dbBlogEntities blogdb = new dbBlogEntities();

        public users AuthenticateUser(string username, string password)
        {
            return blogdb.users.FirstOrDefault(u => u.Username == username &&
                                                    u.Password == password);
        }
        public void Dispose()
        {
            blogdb.Dispose();
        }
    }
}