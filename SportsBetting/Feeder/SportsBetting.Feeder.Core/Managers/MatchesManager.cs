namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;
    using SportsBetting.Services.Data.Contracts;

    public class MatchesManager : IMatchesManager
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper<MatchFeedModel, Match> matchesMapper;
        private readonly IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler;

        public MatchesManager(
            IMatchesService matchesService, 
            IMapper<MatchFeedModel, Match> matchesMapper,
            IQueryHandler<MatchByKeyQuery, Match> matchByKeyHandler)
        {
            this.matchesService = matchesService;
            this.matchesMapper = matchesMapper;
            this.matchByKeyHandler = matchByKeyHandler;
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

            return matchesService.Add(mappedMatch, categoryId, tournamentId, homeTeamId, awayTeamId);
        }
    }
}