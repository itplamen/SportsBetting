namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class MarketsCache : BaseCache<Market>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        private readonly ICacheLoaderRepository<Market> marketsRepository;

        public MarketsCache(ICacheLoaderRepository<Market> marketsRepository)
            :base(REFRESH_INTERVAL)
        {
            this.marketsRepository = marketsRepository;
        }

        public override void Load()
        {
            IEnumerable<Market> markets = marketsRepository.Load(x => !x.IsDeleted && x.CreatedOn.AddDays(7) >= DateTime.UtcNow);

            foreach (var market in markets)
            {
                Add(market.Key, market);
            }
        }

        public override void Refresh()
        {
            IEnumerable<Market> markets = marketsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value.AddMilliseconds(REFRESH_INTERVAL) >= DateTime.UtcNow);

            foreach (var market in markets)
            {
                Update(market.Key, market);
            }
        }
    }
}