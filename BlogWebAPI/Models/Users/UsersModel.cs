using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.Models.Users
{
    public class UsersModel
    {
        //public int Id { get; set; }

        /// <summary>
        /// This value describes the name of the user's account needed to sign in to the application
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// This value represents the password of the user's account needed to sign in to the application
        /// </summary>
        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// This value represents the address of the user's email account
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}