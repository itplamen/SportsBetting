namespace SportsBetting.Server.Api.App_Start
{
    using System.Threading;
    using System.Threading.Tasks;

    using SimpleInjector;

    using SportsBetting.Data.Cache.Contracts;

    public static class CacheConfig
    {
        private readonly static CancellationTokenSource source = new CancellationTokenSource();
        private readonly static CancellationToken token = source.Token;

        public static void Init(Container container)
        {
            ICacheLoader cacheLoader = container.GetInstance<ICacheLoader>();
            cacheLoader.Init();

            Task task = new Task(() => RefreshCaches(cacheLoader));
            task.Start();
        }

        private static void RefreshCaches(ICacheLoader cacheLoader)
        {
            while (!token.IsCancellationRequested)
            {
                cacheLoader.Refresh();
            }
        }
    }
}