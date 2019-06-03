namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data;
    using SportsBetting.Data.Common;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Contracts;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISportsBettingDbContext, SportsBettingDbContext>(Lifestyle.Singleton);
            container.Register(typeof(IRepository<>), typeof(MongoRepository<>), Lifestyle.Singleton);
            container.RegisterDecorator(typeof(IRepository<>), typeof(CacheRepository<>), Lifestyle.Singleton);
        }
    }
}