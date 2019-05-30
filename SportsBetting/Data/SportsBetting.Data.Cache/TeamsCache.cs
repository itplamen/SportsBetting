namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : BaseCache<Team>
    {
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
                Cache[team.Key] = team;
            }
        }
    }
}