namespace SportsBetting.Server.Api.App_Start
{
    using SimpleInjector;

    using SportsBetting.Data.Cache.Contracts;

    public static class CacheConfig
    {
        public static void Init(Container container)
        {
            ICacheLoader cacheLoader = container.GetInstance<ICacheLoader>();

            cacheLoader.Init();
            cacheLoader.Refresh();
        }
    }
}