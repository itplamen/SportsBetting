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
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public TeamsManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            IEnumerable<int> teamKeys = new List<int>() { feedModel.Key };
            EntitiesByKeyQuery<Team> teamQuery = new EntitiesByKeyQuery<Team>(teamKeys);
            Team team = queryDispatcher.Dispatch<EntitiesByKeyQuery<Team>, IEnumerable<Team>>(teamQuery).FirstOrDefault();

            if (team != null)
            {
                return team.Id;
            }

            IEnumerable<int> sportKeys = new List<int>() { CommonConstants.ESPORT_KEY };
            EntitiesByKeyQuery<Sport> sportQuery = new EntitiesByKeyQuery<Sport>(sportKeys);
            Sport sport = queryDispatcher.Dispatch<EntitiesByKeyQuery<Sport>, IEnumerable<Sport>>(sportQuery).First();

            CreateTeamCommand teamCommand = Mapper.Map<CreateTeamCommand>(feedModel);
            teamCommand.SportId = sport.Id;

            return commandDispatcher.Dispatch<CreateTeamCommand, string>(teamCommand);
        }
    }
}