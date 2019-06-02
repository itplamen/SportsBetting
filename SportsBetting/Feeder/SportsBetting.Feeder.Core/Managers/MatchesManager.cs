namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Core.Contracts.Mappers;
    using SportsBetting.Feeder.Models;

    public class MatchesManager : IMatchesManager
    {
        private readonly IRepository<Match> matchesRepository;
        private readonly IMapper<MatchFeedModel, Match> matchesMapper;

        public MatchesManager(IRepository<Match> matchesRepository, IMapper<MatchFeedModel, Match> matchesMapper)
        {
            this.matchesRepository = matchesRepository;
            this.matchesMapper = matchesMapper;
        }

        public string Manage(MatchFeedModel feedModel, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            Match match = matchesRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();
            Match mappedMatch = matchesMapper.Map(feedModel);

            if (match == null)
            {
                return Add(mappedMatch, categoryId, tournamentId, homeTeamId, awayTeamId);
            }

            return Update(mappedMatch);
        }

        private string Add(Match match, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            match.CategoryId = categoryId;
            match.TournamentId = tournamentId;
            match.HomeTeamId = homeTeamId;
            match.AwayTeamId = awayTeamId;

            matchesRepository.Add(match);

            return match.Id;
        }

        private string Update(Match match)
        {
            Match matchToUpdate = matchesRepository.All(x => x.Key == match.Key).FirstOrDefault();
            matchToUpdate.StartTime = match.StartTime;
            matchToUpdate.Score = match.Score;
            match.Status = match.Status;

            matchesRepository.Update(matchToUpdate);

            return matchToUpdate.Id;
        }
    }
}