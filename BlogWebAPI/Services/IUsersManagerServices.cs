using BlogWebAPI.Models;
using BlogWebAPI.Models.Users;
using System.Collections.Generic;

namespace BlogWebAPI.Services
{
    public interface IUserManagerService
    {
        //bool Create(UsersModel userToCreate);
        ResponseModel Create(User userToCreate);
        ResponseModel Edit(int userId, User userToEdit);
        ResponseModel Delete(int userId);
        IEnumerable<object> GetAllUsers();
    }
}
