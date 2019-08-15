namespace SportsBetting.Data.Cache.General
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SportsBetting.Data.Cache.Contracts;

    public class CacheComposite : ICacheLoader
    {
        private const int TIMEOUT = 1000 * 3;

        private readonly IEnumerable<ICacheLoader> caches;

        public CacheComposite(IEnumerable<ICacheLoader> caches)
        {
            this.caches = caches;
        }

        public void Init()
        {
            caches.ToList().ForEach(cache => cache.Init());
        }

        public void Refresh()
        {
            List<Task> task = caches.Select(x => new Task(() => x.Refresh())).ToList();
            task.ForEach(x => x.Start());

            Thread.Sleep(TIMEOUT);
        }
    }
}