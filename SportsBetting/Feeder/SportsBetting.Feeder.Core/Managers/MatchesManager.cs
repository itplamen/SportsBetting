namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Data.Contracts;

    public class MatchesManager : IMatchesManager
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper<MatchFeedModel, Match> matchesMapper;

        public MatchesManager(IMatchesService matchesService, IMapper<MatchFeedModel, Match> matchesMapper)
        {
            this.matchesService = matchesService;
            this.matchesMapper = matchesMapper;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            Match match = matchesService.Get(feedModel.Id);
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