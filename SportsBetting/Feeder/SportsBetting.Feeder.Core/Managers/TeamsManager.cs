namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Common.Constants;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Teams;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TeamsManager : ITeamsManager
    {
        private readonly ICommandHandler<CreateTeamCommand, string> createTeamHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Team>, IEnumerable<Team>> teamByKeyHandler;
        private readonly IQueryHandler<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>> sportByKeyHandler;

        public TeamsManager(
            ICommandHandler<CreateTeamCommand, string> createTeamHandler,
            IQueryHandler<EntitiesByKeyQuery<Team>, IEnumerable<Team>> teamByKeyHandler,
            IQueryHandler<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>> sportByKeyHandler)
        {
            this.createTeamHandler = createTeamHandler;
            this.teamByKeyHandler = teamByKeyHandler;
            this.sportByKeyHandler = sportByKeyHandler;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            IEnumerable<int> teamKeys = new List<int>() { feedModel.Key };
            EntitiesByKeyQuery<Team> teamQuery = new EntitiesByKeyQuery<Team>(teamKeys);
            Team team = teamByKeyHandler.Handle(teamQuery).FirstOrDefault();

            if (team != null)
            {
                return team.Id;
            }

            IEnumerable<int> sportKeys = new List<int>() { CommonConstants.ESPORT_KEY };
            EntitiesByKeyQuery<Sport> sportQuery = new EntitiesByKeyQuery<Sport>(sportKeys);
            Sport sport = sportByKeyHandler.Handle(sportQuery).First();

            CreateTeamCommand teamCommand = Mapper.Map<CreateTeamCommand>(feedModel);
            teamCommand.SportId = sport.Id;

            return createTeamHandler.Handle(teamCommand);
        }
    }
}