namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Common;
    using SportsBetting.Common.Contracts;
    using SportsBetting.Common.Logger;

    public sealed class CommonPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            container.Register<IConfigurationManager, ConfigurationManager>(Lifestyle.Singleton);
        }
    }
}