using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BlogWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "BlogApp/{controller}/{id}/{name}/{id2}",
                defaults: new { id = RouteParameter.Optional, name = RouteParameter.Optional, id2 = RouteParameter.Optional }

            );

            //si se necesitas varios gets/post/ en los controllers 

            //config.Routes.MapHttpRoute("DefaultApiWithId", "BlogApp/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            //config.Routes.MapHttpRoute("DefaultApiWithAction", "BlogApp/{controller}/{action}");
            //config.Routes.MapHttpRoute("DefaultApiGet", "BlogApp/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            //config.Routes.MapHttpRoute("DefaultApiPost", "BlogApp/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}
