namespace SportsBetting.Server.Api
{
    using System.Reflection;
    using System.Web.Http;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Server.Api.App_Start;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SimpleInjectorConfig.RegisterContainer();
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly(), Assembly.Load("SportsBetting.Handlers.Queries"));
        }
    }
}