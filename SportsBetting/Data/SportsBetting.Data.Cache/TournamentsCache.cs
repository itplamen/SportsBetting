namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class TournamentsCache : BaseCache<Tournament>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        public TournamentsCache(ISportsBettingDbContext dbContext)
            : base(dbContext)
        {
        }

        public override void Load()
        {
            IEnumerable<Tournament> tournaments = GetEntities(x => !x.IsDeleted);

            foreach (var tournament in tournaments)
            {
                Add(tournament.Key, tournament);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Tournament> tournaments = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var tournament in tournaments)
            {
                Update(tournament.Key, tournament);
            }
        }
    }
}