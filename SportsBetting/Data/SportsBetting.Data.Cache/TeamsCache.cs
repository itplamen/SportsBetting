namespace SportsBetting.Data.Cache
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.General;
    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : BaseCache<int, Team>
    {
        private readonly IRepository<Team> teamsRepository;

        public TeamsCache(IRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
        }

        public override void Load()
        {
            IEnumerable<Team> teams = teamsRepository.All(x => !x.IsDeleted);

            foreach (var team in teams)
            {
                Cache[team.Key] = team;
            }
        }
    }
}