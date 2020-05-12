using BlogWebAPI.Models.Users;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface IUserManagerService
    {
        //bool Create(UsersModel userToCreate);
        bool Create(UsersModel userToCreate);
        bool Edit(int id, UsersModel userToEdit);
        bool Delete(int id);
        IEnumerable<object> ListOfUsers();
    }
}
