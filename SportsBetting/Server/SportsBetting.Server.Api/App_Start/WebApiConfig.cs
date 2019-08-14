namespace SportsBetting.Server.Api
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AllMatches",
                routeTemplate: "api/{controller}/{action}/{take}",
                defaults: new { controller = "Matches", action = "All" },
                constraints: new { take = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
        }
    }
}