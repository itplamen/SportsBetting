namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class MatchesCache : BaseCache<Match>
    {
        private const int REFRESH_INTERVAL = 1000 * 3;

        private readonly ISportsBettingDbContext dbContext;

        public MatchesCache(ISportsBettingDbContext dbContext)
            : base(REFRESH_INTERVAL)
        {
            this.dbContext = dbContext;
        }

        public override void Init()
        {
            DateTime dateTime = DateTime.UtcNow.AddDays(-7);

            IEnumerable<Match> matches = dbContext.GetCollection<Match>()
                .Find(x => !x.IsDeleted && x.CreatedOn >= dateTime)
                .ToList();

            foreach (var match in matches)
            {
                Add(match.Key, match);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Match> matches = dbContext.GetCollection<Match>()
                .Find(x =>
                    !x.IsDeleted &&
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var match in matches)
            {
                Update(match.Key, match);
            }
        }
    }
}