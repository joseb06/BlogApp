using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.Models
{
    public class PostsModel
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This value must be numeric")]
        public int idAuthor { get; set; }
        [Required]
        [StringLength(200)]
        public string content { get; set; }

    }
}