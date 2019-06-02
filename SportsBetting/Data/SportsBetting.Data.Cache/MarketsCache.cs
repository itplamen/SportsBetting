namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class MarketsCache : BaseCache<Market>
    {
        private readonly ICacheLoaderRepository<Market> marketsRepository;

        public MarketsCache(ICacheLoaderRepository<Market> marketsRepository)
            :base(1000, 1000 * 3)
        {
            this.marketsRepository = marketsRepository;
        }

        public override void Load()
        {
            IEnumerable<Market> markets = marketsRepository.Load(x => !x.IsDeleted);

            foreach (var market in markets)
            {
                Cache[market.Key] = market;
            }
        }
    }
}