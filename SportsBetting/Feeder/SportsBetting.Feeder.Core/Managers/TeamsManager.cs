namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Teams;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Teams;

    public class TeamsManager : ITeamsManager
    {
        private readonly IRepository<Sport> sportsRepository;
        private readonly IQueryHandler<TeamByKeyQuery, Team> teamByKeyHandler;
        private readonly ICommandHandler<CreateTeamCommand, string> createTeamHandler;

        public TeamsManager(
            IRepository<Sport> sportsRepository,
            IQueryHandler<TeamByKeyQuery, Team> teamByKeyHandler,
            ICommandHandler<CreateTeamCommand, string> createTeamHandler)
        {
            this.sportsRepository = sportsRepository;
            this.teamByKeyHandler = teamByKeyHandler;
            this.createTeamHandler = createTeamHandler;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            TeamByKeyQuery query = new TeamByKeyQuery(feedModel.Id);
            Team team = teamByKeyHandler.Handle(query);

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