namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class SportsCache : BaseCache<Sport>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        public SportsCache(ISportsBettingDbContext dbContext)
            : base(dbContext)
        {
        }

        public override void Load()
        {
            IEnumerable<Sport> sports = GetEntities(x => !x.IsDeleted);

            foreach (var sport in sports)
            {
                Add(sport.Key, sport);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Sport> sports = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var sport in sports)
            {
                Update(sport.Key, sport);
            }
        }
    }
}