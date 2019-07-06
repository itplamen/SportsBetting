namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Teams;
    using SportsBetting.Services.Data.Contracts;

    public class TeamsManager : ITeamsManager
    {
        private readonly ITeamsService teamsService;
        private readonly IRepository<Sport> sportsRepository;
        private readonly ICommandHandler<CreateTeamCommand, string> createTeamHandler;

        public TeamsManager(ITeamsService teamsService, IRepository<Sport> sportsRepository, ICommandHandler<CreateTeamCommand, string> createTeamHandler)
        {
            this.teamsService = teamsService;
            this.sportsRepository = sportsRepository;
            this.createTeamHandler = createTeamHandler;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            Team team = teamsService.Get(feedModel.Id);

            if (team != null)
            {
                return team.Id;
            }

            Sport sport = sportsRepository.All(x => x.Key == 1).FirstOrDefault();

            CreateTeamCommand command = new CreateTeamCommand()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                Score = feedModel.Score,
                SportId = sport.Id
            };

            return createTeamHandler.Handle(command);
        }
    }
}