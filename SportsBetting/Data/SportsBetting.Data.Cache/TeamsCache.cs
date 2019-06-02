namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : BaseCache<Team>
    {
        private const int REFRESH_INTERVAL = 1000 * 60;

        private readonly ICacheLoaderRepository<Team> teamsRepository;

        public TeamsCache(ICacheLoaderRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
        }

        public override void Load()
        {
            IEnumerable<Team> teams = teamsRepository.Load(x => !x.IsDeleted);

            foreach (var team in teams)
            {
                Add(team.Key, team);
            }
        }

        public override void Refresh()
        {
            IEnumerable<Team> teams = teamsRepository.Load(x =>
                !x.IsDeleted &&
                x.ModifiedOn.HasValue &&
                x.ModifiedOn.Value.AddMilliseconds(REFRESH_INTERVAL) >= DateTime.UtcNow);

            foreach (var team in teams)
            {
                Update(team.Key, team);
            }
        }
    }
}