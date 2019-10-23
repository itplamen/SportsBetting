namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using SportsBetting.Data.Cache.Contracts;

    public class CacheComposite : ICacheLoader
    {
        private const int TIME_INTERVAL = 1000 * 3;

        private readonly ManualResetEvent resetEvent;
        private readonly IEnumerable<ICacheLoader> caches;

        public CacheComposite(IEnumerable<ICacheLoader> caches)
        {
            this.caches = caches;
            this.resetEvent = new ManualResetEvent(false);
        }

        public void Init()
        {
            caches.ToList().ForEach(cache => cache.Init());
        }

        public void Refresh()
        {
            ThreadPool.RegisterWaitForSingleObject(
                resetEvent,
                new WaitOrTimerCallback((x, y) => caches.ToList().ForEach(z => z.Refresh())),
                null,
                TIME_INTERVAL,
                false);
        }
    }
}