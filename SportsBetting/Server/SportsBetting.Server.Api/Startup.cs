using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;

using Owin;

using SimpleInjector;

using SportsBetting.Server.Api.App_Start;
using SportsBetting.Server.Api.Hubs;

[assembly: OwinStartup(typeof(SportsBetting.Server.Api.Startup))]

namespace SportsBetting.Server.Api
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