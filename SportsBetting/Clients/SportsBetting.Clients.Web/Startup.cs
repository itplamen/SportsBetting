using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;

using Owin;

using SimpleInjector;

using SportsBetting.Clients.Web.App_Start;
using SportsBetting.Clients.Web.Hubs;

[assembly: OwinStartup(typeof(SportsBetting.Clients.Web.Startup))]
namespace SportsBetting.Clients.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Container container = SimpleInjectorConfig.RegisterContainer();
            IHubActivator activator = new SimpleInjectorHubActivator(container);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);

            app.MapSignalR();
        }
    }
}