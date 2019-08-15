namespace SportsBetting.IoCContainer.Packages.Common
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.Contracts;

using SportsBetting.Data.Models;

    public sealed class DataCachePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(ICache<>), typeof(Cache<>), Lifestyle.Singleton);

            container.Register<ICacheLoader, CacheComposite>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Sport>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Category>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Tournament>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Team>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Match>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Market>>(Lifestyle.Singleton);
            container.Collection.Append<ICacheLoader, Cache<Odd>>(Lifestyle.Singleton);
        }
    }
}