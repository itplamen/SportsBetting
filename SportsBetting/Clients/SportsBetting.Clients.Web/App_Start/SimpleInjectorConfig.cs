namespace SportsBetting.Clients.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Packaging;

    using SportsBetting.IoCContainer.Packages.Web;

    public static class SimpleInjectorConfig
    {
        public static void RegisterContainer()
        {
            Container container = new Container();

            IPackage[] packages = new IPackage[]
            {
                new DataPackage(),
                new DataCachePackage(),
                new DataServicesPackage(),
                new QueryHandlersPackage(),
                new CommandHandlersPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}