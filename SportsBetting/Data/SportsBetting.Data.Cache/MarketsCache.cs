namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class MarketsCache : BaseCache<Market>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        public MarketsCache(ISportsBettingDbContext dbContext)
            : base(dbContext, REFRESH_INTERVAL)
        {
        }

        public override void Load()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);

            IEnumerable<Market> markets = GetEntities(x => !x.IsDeleted && x.CreatedOn >= dateTime);

            foreach (var market in markets)
            {
                Add(market.Key, market);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Market> markets = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var market in markets)
            {
                Update(market.Key, market);
            }
        }
    }
}