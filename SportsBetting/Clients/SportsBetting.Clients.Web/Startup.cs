using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SportsBetting.Clients.Web.Startup))]
namespace SportsBetting.Clients.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}