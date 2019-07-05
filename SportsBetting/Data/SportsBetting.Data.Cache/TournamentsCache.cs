namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class TournamentsCache : BaseCache<Tournament>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ISportsBettingDbContext dbContext;

        public TournamentsCache(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Load()
        {
            IEnumerable<Tournament> tournaments = dbContext.GetCollection<Tournament>()
                .Find(x => !x.IsDeleted)
                .ToList();

            foreach (var tournament in tournaments)
            {
                Add(tournament.Key, tournament);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Tournament> tournaments = dbContext.GetCollection<Tournament>()
                .Find(x =>
                    !x.IsDeleted &&
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var tournament in tournaments)
            {
                Update(tournament.Key, tournament);
            }
        }
    }
}