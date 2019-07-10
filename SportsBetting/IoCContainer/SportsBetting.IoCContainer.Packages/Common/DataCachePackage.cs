namespace SportsBetting.IoCContainer.Packages.Common
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Cache.General;

    public sealed class DataCachePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(ICache<>), typeof(SportsCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(CategoriesCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(TournamentsCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(TeamsCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(MatchesCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(MarketsCache), Lifestyle.Singleton);
            container.Register(typeof(ICache<>), typeof(OddsCache), Lifestyle.Singleton);

            container.Register<ICacheInitializer, CacheComposite>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, SportsCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, CategoriesCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, TournamentsCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, TeamsCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, MatchesCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, MarketsCache>(Lifestyle.Singleton);
            container.Collection.Append<ICacheInitializer, OddsCache>(Lifestyle.Singleton);
        }
    }
}