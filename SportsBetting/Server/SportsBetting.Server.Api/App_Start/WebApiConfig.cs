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
               defaults: new { id = RouteParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
           );

            config.Routes.MapHttpRoute(
                name: "Register",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Account", action = "Register" }
            );

            config.Routes.MapHttpRoute(
                name: "AllMatches",
                routeTemplate: "api/{controller}/{action}/{take}",
                defaults: new { controller = "Matches", action = "All" },
                constraints: new { take = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "Login",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = "Auth", action = "Login" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "Logout",
                routeTemplate: "api/{controller}/{action}/{loginToken}",
                defaults: new { controller = "Auth", action = "Logout" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );
        }
    }
}