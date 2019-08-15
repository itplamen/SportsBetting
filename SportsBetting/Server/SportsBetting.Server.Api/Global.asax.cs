namespace SportsBetting.Server.Api
{
     using System.Reflection;
    using System.Web.Http;

    using SimpleInjector;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Server.Api.App_Start;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Container container = new Container();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            SimpleInjectorConfig.RegisterContainer(container);
            CacheConfig.Init(container);
            AutoMapperConfig.RegisterMappings(
                Assembly.GetExecutingAssembly(), 
                Assembly.Load("SportsBetting.Handlers.Queries"), 
                Assembly.Load("SportsBetting.Handlers.Commands"));
        }
    }
}