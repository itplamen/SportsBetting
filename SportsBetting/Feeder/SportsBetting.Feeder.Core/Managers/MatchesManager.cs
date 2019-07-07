namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Matches;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;

    public class MatchesManager : IMatchesManager
    {
        private readonly IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler;
        private readonly ICommandHandler<UpdateMatchCommand, string> updateMatchHandler;
        private readonly ICommandHandler<CreateMatchCommand, string> createMatchHandler;

        public MatchesManager(
            IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler,
            ICommandHandler<UpdateMatchCommand, string> updateMatchHandler,
            ICommandHandler<CreateMatchCommand, string> createMatchHandler)
        {
            this.matchByKeyHandler = matchByKeyHandler;
            this.updateMatchHandler = updateMatchHandler;
            this.createMatchHandler = createMatchHandler;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            MatchByKeyQuery query = new MatchByKeyQuery(feedModel.Id);
            Match match = matchByKeyHandler.Handle(query);

            if (match != null)
            {
                UpdateMatchCommand updateCommand = new UpdateMatchCommand()
                {
                    Id = match.Id,
                    Score = $"{feedModel.HomeTeam.Score}:{feedModel.AwayTeam.Score}",
                    StartTime = feedModel.StartTime
                };

                return updateMatchHandler.Handle(updateCommand);
            }

            CreateMatchCommand createCommand = new CreateMatchCommand()
            {
                Key = feedModel.Id,
                Score = $"{feedModel.HomeTeam.Score}:{feedModel.AwayTeam.Score}",
                StartTime = feedModel.StartTime,
                CategoryId = categoryId,
                TournamentId = tournamentId,
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId
            };

            return createMatchHandler.Handle(createCommand);
        }
    }
}