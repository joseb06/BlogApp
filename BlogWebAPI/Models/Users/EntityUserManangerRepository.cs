using BlogDBSQLServer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Users
{
    public class EntityUserManangerRepository : IUserManagerRepository
    {
        private readonly dbBlogEntities blogdb = new dbBlogEntities();

        public users Create(User userToCreate)
        {
            var user = new users
            {
                Username = userToCreate.Username,
                Password = userToCreate.Password,
                Email = userToCreate.Email
            };
            blogdb.Entry(user).State = EntityState.Added;
            blogdb.SaveChanges();

            return user;
        }

        public bool Delete(int userId)
        {
            var user = (from users in blogdb.users
                        where users.Id == userId
                        select users).SingleOrDefault();

            if (user != null)
            {
                blogdb.Entry(user).State = EntityState.Deleted;
                blogdb.SaveChanges();
                return true;
            }

            return false;
        }

        public users Edit(int userId, User userToEdit)
        {
            var user = (from users in blogdb.users
                        where users.Id == userId
                        select users).SingleOrDefault();

            if (user != null)
            {
                user.Username = userToEdit.Username;
                user.Password = userToEdit.Password;
                user.Email = userToEdit.Email;

                blogdb.Entry(user).State = EntityState.Modified;
                blogdb.SaveChanges();
            }
            return user;
        }

        public IEnumerable<object> GetAllUsers()
        {
            return blogdb.users.ToList();
        }
    }
}