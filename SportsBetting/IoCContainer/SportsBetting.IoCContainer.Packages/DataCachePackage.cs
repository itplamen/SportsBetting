namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data;
    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Contracts;

    public sealed class DataCachePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            RegisterRepositories(container);
            RegisterCaches(container);
        }

        private void RegisterRepositories(Container container)
        {
            container.Register<ISportsBettingDbContext, SportsBettingDbContext>(Lifestyle.Singleton);
            container.Register(typeof(IRepository<>), typeof(MongoRepository<>), Lifestyle.Singleton);
            container.RegisterDecorator(typeof(IRepository<>), typeof(CacheRepository<>), Lifestyle.Singleton);
            container.Register(typeof(ICacheLoaderRepository<>), typeof(CacheLoaderRepository<>), Lifestyle.Singleton);
        }

        private void RegisterCaches(Container container)
        {
            container.Register(typeof(ICache<>), typeof(CategoriesCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(TeamsCache), Lifestyle.Singleton);
            container.Collection.Register<ICacheInitializer>(typeof(CategoriesCache), typeof(TeamsCache));
            container.Register<ICacheInitializer, CacheComposite>(Lifestyle.Singleton);
        }
    }
}