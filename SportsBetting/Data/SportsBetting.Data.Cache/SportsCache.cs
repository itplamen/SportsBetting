namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class SportsCache : BaseCache<Sport>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ISportsBettingDbContext dbContext;

        public SportsCache(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Load()
        {
            IEnumerable<Sport> sports = dbContext.GetCollection<Sport>()
                .Find(x => !x.IsDeleted)
                .ToList();

            foreach (var sport in sports)
            {
                Add(sport.Key, sport);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Sport> sports = dbContext.GetCollection<Sport>()
                .Find(x =>
                    !x.IsDeleted &&
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var sport in sports)
            {
                Update(sport.Key, sport);
            }
        }
    }
}