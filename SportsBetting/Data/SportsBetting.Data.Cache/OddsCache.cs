namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    
    public class OddsCache : BaseCache<Odd>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        public OddsCache(ISportsBettingDbContext dbContext)
            : base(dbContext, REFRESH_INTERVAL)
        {
        }

        public override void Load()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);

            IEnumerable<Odd> odds = GetEntities(x => !x.IsDeleted && x.CreatedOn >= dateTime);

            foreach (var odd in odds)
            {
                Add(odd.Key, odd);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Odd> odds = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var odd in odds)
            {
                Update(odd.Key, odd);
            }
        }
    }
}