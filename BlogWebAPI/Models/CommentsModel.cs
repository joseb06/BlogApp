using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWebAPI.Models
{
    public class CommentsModel
    {
        public int id { get; set; }
        public int id_post { get; set; }
        public int id_commentarist { get; set; }
        public string comment{ get; set; }
    }
}