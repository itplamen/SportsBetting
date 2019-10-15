namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Matches;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchesManager : IMatchesManager
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public MatchesManager(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            IEnumerable<int> keys = new List<int>() { feedModel.Key };
            EntitiesByKeyQuery<Match> matchQuery = new EntitiesByKeyQuery<Match>(keys);
            Match match = queryDispatcher.Dispatch<EntitiesByKeyQuery<Match>, IEnumerable<Match>>(matchQuery).FirstOrDefault();

            if (match != null)
            {
                UpdateMatchCommand updateCommand = Mapper.Map<UpdateMatchCommand>(feedModel);
                updateCommand.Id = match.Id;

                return commandDispatcher.Dispatch<UpdateMatchCommand, string>(updateCommand);
            }

            CreateMatchCommand createCommand = Mapper.Map<CreateMatchCommand>(feedModel);
            createCommand.CategoryId = categoryId;
            createCommand.TournamentId = tournamentId;
            createCommand.HomeTeamId = homeTeamId;
            createCommand.AwayTeamId = awayTeamId;

            return commandDispatcher.Dispatch<CreateMatchCommand, string>(createCommand);
        }
    }
}