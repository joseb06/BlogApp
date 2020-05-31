using BlogWebAPI.Models;
using BlogWebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace BlogWebAPI.Services
{

    public class UsersManagerServices : IUserManagerService
    {
        private readonly IUserManagerRepository _repository;
        public UsersManagerServices()
        { }

        public UsersManagerServices(IUserManagerRepository repository)
        {
            _repository = repository;
        }

        public int GetLoginUserId()
        {
            var loggedUserData = ClaimsPrincipal.Current;
            return int.Parse(loggedUserData.FindFirst("userID").Value);
        }

        public ResponseModel Create(User userToCreate)
        {
            try
            {
                _repository.Create(userToCreate);
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "User was created successfully."
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User cannot be created."
                };
            }
        }

        public ResponseModel Delete(int userId)
        {
            try
            {
                if (!_repository.Delete(userId))
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a User with ID = {0}", userId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "User was removed successfully."
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Post cannot be removed."
                };
            }

        }

        public ResponseModel Edit(int userId, User userToEdit)
        {
            try
            {
                if (_repository.Edit(userId, userToEdit) == null)
                {
                    return new ResponseModel()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = string.Format("Does not exist a User with ID = {0}", userId)
                    };
                }
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "User was updated successfully"
                };
            }
            catch (Exception ex)
            {
                new ErrorLogManager().SaveError(this, ex);

                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "User cannot be updated"
                };
            }
        }

        public IEnumerable<object> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }
    }
}