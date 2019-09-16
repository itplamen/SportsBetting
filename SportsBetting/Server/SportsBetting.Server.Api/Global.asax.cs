namespace SportsBetting.Server.Api
{
    using System.Reflection;
    using System.Web.Http;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using SimpleInjector;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Server.Api.App_Start;
    using SportsBetting.Server.Api.Hubs;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Container container = SimpleInjectorConfig.RegisterContainer();
            CacheConfig.Init(container);
            AutoMapperConfig.RegisterMappings(
                Assembly.GetExecutingAssembly(),
                Assembly.Load("SportsBetting.Handlers.Queries"),
                Assembly.Load("SportsBetting.Handlers.Commands"));

            IHubActivator activator = new SimpleInjectorHubActivator(container);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);
        }
    }
}