namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Factories;
    using SportsBetting.Services.Feeder.Services;

    public sealed class FeederPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            RegisterFactories(container);
            RegisterFeederServices(container);
        }

        private void RegisterFactories(Container container)
        {
            container.Register<IWebDriverFactory, WebDriverFactory>(Lifestyle.Singleton);
            container.Register<IObjectFactory, ObjectFactory>(Lifestyle.Singleton);
        }

        private void RegisterFeederServices(Container container)
        {
            container.Register<IWebPagesService, WebPagesService>(Lifestyle.Singleton);
        }
    }
}