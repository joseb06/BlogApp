using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWebAPI.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This value must be numeric")]
        public int Id_post { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This value must be numeric")]
        public int Id_commentarist { get; set; }

        [Required]
        [StringLength(2000)]
        public string Comment{ get; set; }
    }
}