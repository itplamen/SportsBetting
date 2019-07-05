namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;

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

            container.Collection.Register<ICacheInitializer>(
                typeof(SportsCache),
                typeof(CategoriesCache),
                typeof(TournamentsCache),
                typeof(TeamsCache),
                typeof(MatchesCache),
                typeof(MarketsCache),
                typeof(OddsCache));

            container.Register<ICacheInitializer, CacheComposite>(Lifestyle.Singleton);
        }
    }
}