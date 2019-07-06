namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Matches;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;
    using SportsBetting.Services.Data.Contracts;

    public class MatchesManager : IMatchesManager
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper<MatchFeedModel, Match> matchesMapper;
        private readonly IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler;
        private readonly ICommandHandler<CreateMatchCommand, string> createMatchHandler;

        public MatchesManager(
            IMatchesService matchesService, 
            IMapper<MatchFeedModel, Match> matchesMapper,
            IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler,
            ICommandHandler<CreateMatchCommand, string> createMatchHandler)
        {
            this.matchesService = matchesService;
            this.matchesMapper = matchesMapper;
            this.matchByKeyHandler = matchByKeyHandler;
            this.createMatchHandler = createMatchHandler;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            MatchByKeyQuery query = new MatchByKeyQuery(feedModel.Id);
            Match match = matchByKeyHandler.Handle(query);
            Match mappedMatch = matchesMapper.Map(feedModel);

            if (match != null)
            {
                matchesService.Update(match.Id, mappedMatch);

                return match.Id;
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