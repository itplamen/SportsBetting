namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Cache;
    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Cache.General;

    public sealed class DataCachePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(ICache<>), typeof(SportsCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(CategoriesCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(TournamentsCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(TeamsCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(MatchesCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(MarketsCache), new WebRequestLifestyle());
            container.Register(typeof(ICache<>), typeof(OddsCache), new WebRequestLifestyle());

            container.Register<ICacheInitializer, CacheComposite>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, SportsCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, CategoriesCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, TournamentsCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, TeamsCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, MatchesCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, MarketsCache>(new WebRequestLifestyle());
            container.Collection.Append<ICacheInitializer, OddsCache>(new WebRequestLifestyle());
        }
    }
}