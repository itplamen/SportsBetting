namespace SportsBetting.Server.Api.App_Start
{
    using System.Web.Http;

    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.IoCContainer.Packages.Common;
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
                new QueryHandlersPackage(),
                new CommandHandlersPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            ICacheInitializer cacheInitializer = container.GetInstance<ICacheInitializer>();
            cacheInitializer.Init();
        }
    }
}