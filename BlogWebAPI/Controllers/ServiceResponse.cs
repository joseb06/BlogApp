using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWebAPI.Controllers
{
    public class ServiceResponse
    {
        public int status { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}