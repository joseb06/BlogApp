using Autofac;
using Autofac.Integration.WebApi;
using BlogWebAPI.Models.Comments;
using BlogWebAPI.Models.Posts;
using BlogWebAPI.Models.Users;
using BlogWebAPI.Services;
using System.Reflection;
using System.Web.Http;

namespace BlogWebAPI.App_Start
{
    public static class IoCConfig
    {
        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterControllers(builder);

            Container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UsersManagerServices>().As<IUserManagerService>().SingleInstance();
            builder.RegisterType<PostsManagerServices>().As<IPostsManagerServices>().SingleInstance();
            builder.RegisterType<CommentsManagerServices>().As<ICommentsManagerServices>().SingleInstance();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<EntityUserManangerRepository>().As<IUserManagerRepository>().SingleInstance();
            builder.RegisterType<EntityPostManagerRepository>().As<IPostManagerRepository>().SingleInstance();
            builder.RegisterType<EntityCommentsManangerRepository>().As<ICommentsManagerRepository>().SingleInstance();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}