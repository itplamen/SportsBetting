namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common;
    using SportsBetting.Data.Common.Contracts;

    public sealed class DataCachePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(ICacheLoaderRepository<>), typeof(CacheLoaderRepository<>), Lifestyle.Singleton);

            container.Collection.Register(typeof(ICache <,>),
                typeof(CategoriesCache),
                typeof(TeamsCache));

            container.Register(typeof(ICache<,>), typeof(CacheComposite), Lifestyle.Singleton);
        }
    }
}