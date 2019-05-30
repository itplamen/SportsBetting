namespace SportsBetting.Data.Cache.General
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;

    public class CacheComposite : ICacheInitializer
    {
        private readonly IEnumerable<ICacheInitializer> caches;

        public CacheComposite(IEnumerable<ICacheInitializer> caches)
        {
            this.caches = caches;
        }

        public void Init()
        {
            caches.ToList().ForEach(cache => cache.Init());
        }
    }
}