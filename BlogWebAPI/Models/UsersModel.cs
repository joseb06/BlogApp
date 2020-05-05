using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        

        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        [PasswordPropertyText]
        public string Password { get; set; }
        
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}