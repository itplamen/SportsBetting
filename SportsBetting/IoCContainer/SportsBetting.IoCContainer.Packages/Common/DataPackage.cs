namespace SportsBetting.IoCContainer.Packages.Common
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Data;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Initialize;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISportsBettingDbContext, SportsBettingDbContext>(Lifestyle.Singleton);
            container.Register<IAplicationInitializer, DbInitializer>(Lifestyle.Singleton);
        }
    }
}