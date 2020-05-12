using BlogDatabase.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Models.Users
{
    public interface IUserManagerRepository
    {
        users Create(UsersModel userToCreate);
        users Edit(int id, UsersModel userToEdit);
        bool Delete(int id);
        IEnumerable<object> ListOfUsers();
    }
}