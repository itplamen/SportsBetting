namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : BaseCache<Team>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ISportsBettingDbContext dbContext;

        public TeamsCache(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Init()
        {
            IEnumerable<Team> teams = dbContext.GetCollection<Team>()
                .Find(x => !x.IsDeleted)
                .ToList();

            foreach (var team in teams)
            {
                Add(team.Key, team);
            }
        }

        public override void Refresh()
        {
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(-REFRESH_INTERVAL);

            IEnumerable<Team> teams = dbContext.GetCollection<Team>()
                .Find(x =>
                    !x.IsDeleted &&
                    x.ModifiedOn.HasValue &&
                    x.ModifiedOn.Value >= dateTime)
                .ToList();

            foreach (var team in teams)
            {
                Update(team.Key, team);
            }
        }
    }
}