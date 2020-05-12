using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.Models.Posts
{
    public class PostsModel
    {
        //public int id { get; set; }


        /// <summary>
        /// Identificator of the user who created a post
        /// </summary>
        //[Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "This value must be numeric")]
        //public int IdAuthor { get; set; }

        /// <summary>
        /// Subject of post
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Content { get; set; }

    }
}