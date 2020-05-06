using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWebAPI.Models
{
    public class CommentsModel
    {
        //public int Id { get; set; }
        //public int Id_post { get; set; }
        //public int Id_commentarist { get; set; }

        /// <summary>
        /// Personal content made about the post
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Comment{ get; set; }
    }
}