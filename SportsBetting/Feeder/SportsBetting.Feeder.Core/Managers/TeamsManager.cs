﻿namespace SportsBetting.Feeder.Core.Managers
{
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
        private readonly IQueryHandler<EntityByKeyQuery<Team>, Team> teamByKeyHandler;
        private readonly IQueryHandler<EntityByKeyQuery<Sport>, Sport> sportByKeyHandler;

        public TeamsManager(
            ICommandHandler<CreateTeamCommand, string> createTeamHandler,
            IQueryHandler<EntityByKeyQuery<Team>, Team> teamByKeyHandler,
            IQueryHandler<EntityByKeyQuery<Sport>, Sport> sportByKeyHandler)
        {
            this.createTeamHandler = createTeamHandler;
            this.teamByKeyHandler = teamByKeyHandler;
            this.sportByKeyHandler = sportByKeyHandler;
        }

        public string Manage(TeamFeedModel feedModel)
        {
            EntityByKeyQuery<Team> teamQuery = new EntityByKeyQuery<Team>(feedModel.Key);
            Team team = teamByKeyHandler.Handle(teamQuery);

            if (team != null)
            {
                return team.Id;
            }
            
            EntityByKeyQuery<Sport> sportQuery = new EntityByKeyQuery<Sport>(CommonConstants.ESPORT_KEY);
            Sport sport = sportByKeyHandler.Handle(sportQuery);

            CreateTeamCommand teamCommand = Mapper.Map<CreateTeamCommand>(feedModel);
            teamCommand.SportId = sport.Id;

            return createTeamHandler.Handle(teamCommand);
        }
    }
}