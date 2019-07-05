namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : BaseCache<Team>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        public TeamsCache(ISportsBettingDbContext dbContext)
            : base(dbContext)
        {
        }

        public override void Load()
        {
            IEnumerable<Team> teams = GetEntities(x => !x.IsDeleted);

            foreach (var team in teams)
            {
                Add(team.Key, team);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Team> teams = GetEntities(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value >= dateTime);

            foreach (var team in teams)
            {
                Update(team.Key, team);
            }
        }
    }
}