using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(SportsBetting.Server.Api.Startup))]

namespace SportsBetting.Server.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
        }
    }
}