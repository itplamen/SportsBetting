namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class MarketsCache : BaseCache<Market>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        private readonly ISportsBettingDbContext dbContext;

        public MarketsCache(ISportsBettingDbContext dbContext)
            : base(REFRESH_INTERVAL)
        {
            this.dbContext = dbContext;
        }

        public override void Load()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);

            IEnumerable<Market> markets = dbContext.GetCollection<Market>()
                .Find(x => !x.IsDeleted && x.CreatedOn >= dateTime)
                .ToList();

            foreach (var market in markets)
            {
                Add(market.Key, market);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Market> markets = dbContext.GetCollection<Market>()
                .Find(x =>
                    !x.IsDeleted &&
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var market in markets)
            {
                Update(market.Key, market);
            }
        }
    }
}