namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Data.Contracts;

    public class TeamsManager : ITeamsManager
    {
        private readonly ITeamsService teamsService;
        private readonly IRepository<Sport> sportsRepository;

        public TeamsManager(ITeamsService teamsService, IRepository<Sport> sportsRepository)
        {
            this.teamsService = teamsService;
            this.sportsRepository = sportsRepository;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            Team team = teamsService.Get(feedModel.Id);

            if (team != null)
            {
                return team.Id;
            }

            Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

            return teamsService.Add(feedModel.Id, feedModel.Name, sport.Id);
        }
    }
}