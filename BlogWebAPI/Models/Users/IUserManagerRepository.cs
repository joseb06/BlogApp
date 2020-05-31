using BlogDBSQLServer.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Users
{
    public interface IUserManagerRepository
    {
        users Create(User userToCreate);
        users Edit(int userId, User userToEdit);
        bool Delete(int userId);
        IEnumerable<object> GetAllUsers();
    }
}