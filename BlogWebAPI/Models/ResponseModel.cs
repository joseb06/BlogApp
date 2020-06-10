using System.Net;

namespace BlogWebAPI.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}