namespace SportsBetting.Data.Cache.General
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Models.Base;

    public class CacheComposite : BaseCache<int, BaseModel>
    {
        private readonly IEnumerable<BaseCache<int, BaseModel>> caches;

        public CacheComposite(IEnumerable<BaseCache<int, BaseModel>> caches)
        {
            this.caches = caches;
        }

        public override void Load()
        {
            caches.ToList().ForEach(cache => cache.Load());
        }
    }
}