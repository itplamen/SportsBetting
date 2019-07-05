namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class MatchesCache : BaseCache<Match>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        public MatchesCache(ISportsBettingDbContext dbContext)
            : base(dbContext, REFRESH_INTERVAL)
        {
        }

        public override void Load()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);

            IEnumerable<Match> matches = GetEntities(x => !x.IsDeleted && x.CreatedOn >= dateTime);

            foreach (var match in matches)
            {
                Add(match.Key, match);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Match> matches = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var match in matches)
            {
                Update(match.Key, match);
            }
        }
    }
}