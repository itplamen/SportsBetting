namespace SportsBetting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TeamsService : ITeamsService
    {
        private readonly IRepository<Team> teamsRepository;

        public TeamsService(IRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
        }

        public Team Get(int key)
        {
            Team team = teamsRepository.All(x => x.Key == key).FirstOrDefault();

            return team;
        }

        public IEnumerable<Team> Get(IEnumerable<string> teamIds)
        {
            IEnumerable<Team> teams = teamsRepository.All(x => !x.IsDeleted && teamIds.Contains(x.Id));

            return teams;
        }

        public IEnumerable<Team> AllWithDeleted()
        {
            IEnumerable<Team> teams = teamsRepository.All(x => true);

            return teams;
        }
    }
}