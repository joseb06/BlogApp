using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.Models.Comments
{
    public class Comment
    {
        //public int Id { get; set; }
        //public int Id_post { get; set; }
        //public int Id_commentarist { get; set; }

        /// <summary>
        /// Personal content made about the post
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Comments { get; set; }
    }
}