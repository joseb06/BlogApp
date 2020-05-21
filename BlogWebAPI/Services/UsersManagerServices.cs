using BlogWebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BlogWebAPI.Services
{
    public class UsersManagerServices : IUserManagerService
    {
        private readonly IUserManagerRepository repository;
        ErrorLogManager LogManager;
        //public UsersManagerServices() : this(new EntityUserManangerRepository())
        //{ }
        public UsersManagerServices()
        { }
        public UsersManagerServices(IUserManagerRepository repository)
        {
            this.repository = repository;
        }
        public int GetLoginUserId()
        {
            var loggedUserData = ClaimsPrincipal.Current;
            return int.Parse(loggedUserData.FindFirst("userID").Value);
        }
        public bool Create(UsersModel userToCreate)
        {
            try
            {
                repository.Create(userToCreate);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
            
        }

        public bool Edit(int id, UsersModel userToEdit)
        {
            try
            {
                if(repository.Edit(id, userToEdit) == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return false;
            }
        }

        public IEnumerable<object> ListOfUsers()
        {
            try
            {
                return repository.ListOfUsers();
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);
                return null;
            }

        }
    }
}