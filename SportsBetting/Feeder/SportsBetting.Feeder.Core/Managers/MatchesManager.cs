namespace SportsBetting.Feeder.Core.Managers
{
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
        private readonly ICommandHandler<UpdateMatchCommand, string> updateMatchHandler;
        private readonly ICommandHandler<CreateMatchCommand, string> createMatchHandler;
        private readonly IQueryHandler<EntityByKeyQuery<Match>, Match> matchByKeyHandler;

        public MatchesManager(
            ICommandHandler<UpdateMatchCommand, string> updateMatchHandler,
            ICommandHandler<CreateMatchCommand, string> createMatchHandler,
            IQueryHandler<EntityByKeyQuery<Match>, Match> matchByKeyHandler)
        {
            this.updateMatchHandler = updateMatchHandler;
            this.createMatchHandler = createMatchHandler;
            this.matchByKeyHandler = matchByKeyHandler;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            EntityByKeyQuery<Match> matchQuery = new EntityByKeyQuery<Match>(feedModel.Key);
            Match match = matchByKeyHandler.Handle(matchQuery);

            if (match != null)
            {
                UpdateMatchCommand updateCommand = Mapper.Map<UpdateMatchCommand>(feedModel);
                updateCommand.Id = match.Id;

                return updateMatchHandler.Handle(updateCommand);
            }

            CreateMatchCommand createCommand = Mapper.Map<CreateMatchCommand>(feedModel);
            createCommand.CategoryId = categoryId;
            createCommand.TournamentId = tournamentId;
            createCommand.HomeTeamId = homeTeamId;
            createCommand.AwayTeamId = awayTeamId;

            return createMatchHandler.Handle(createCommand);
        }
    }
}