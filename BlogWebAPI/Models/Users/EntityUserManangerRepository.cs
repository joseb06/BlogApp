using BlogDatabase.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlogWebAPI.Models.Users
{
    public class EntityUserManangerRepository : IUserManagerRepository
    {
        private readonly blogdbEntities blogdb = new blogdbEntities();

        public users Create(UsersModel userToCreate)
        {
            int lastUserId = blogdb.users.OrderByDescending(u => u.id).First().id;
            int nextUserId = lastUserId + 1;

            users newUser = new users
            {
                id = nextUserId,
                userName = userToCreate.UserName,
                password = userToCreate.Password,
                email = userToCreate.Email
            };
            blogdb.Entry(newUser).State = EntityState.Added;
            blogdb.SaveChanges();

            return newUser;
        }

        public bool Delete(int id)
        {
            users newUser = (from u in blogdb.users
                             where u.id == id
                             select u).SingleOrDefault();
            if (newUser == null)
            {
                return false;
            }
            blogdb.Entry(newUser).State = EntityState.Deleted;
            blogdb.SaveChanges();

            return true;
        }

        public users Edit(int id, UsersModel userToEdit)
        {
            users newUser = (from u in blogdb.users
                             where u.id == id
                             select u).SingleOrDefault();
            if (newUser != null)
            {
                newUser.userName = userToEdit.UserName;
                newUser.password = userToEdit.Password;
                newUser.email = userToEdit.Email;

                blogdb.Entry(newUser).State = EntityState.Modified;
                blogdb.SaveChanges();

                return newUser;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> ListOfUsers()
        {
            return blogdb.users.ToList();
        }
    }
}