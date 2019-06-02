namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;

    public class TeamsManager : ITeamsManager
    {
        private readonly IRepository<Team> teamsRepository;
        private readonly IRepository<Sport> sportsRepository;

        public TeamsManager(IRepository<Team> teamsRepository, IRepository<Sport> sportsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.sportsRepository = sportsRepository;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            Team team = teamsRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();

            if (team == null)
            {
                Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

                return Add(feedModel, sport.Id);
            }

            return team.Id;
        }

        private string Add(TeamFeedModel feedModel, string sportId)
        {
            Team team = new Team()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                SportId = sportId
            };

            teamsRepository.Add(team);

            return team.Id;
        }
    }
}