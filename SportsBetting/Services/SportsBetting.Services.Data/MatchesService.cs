namespace SportsBetting.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class MatchesService : IMatchesService
    {
        private readonly IRepository<Match> matchesRepository;

        public MatchesService(IRepository<Match> matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public string Add(Match match, string categoryId, string tournamentId, string homeTeamId, string awayTeamId)
        {
            match.CategoryId = categoryId;
            match.TournamentId = tournamentId;
            match.HomeTeamId = homeTeamId;
            match.AwayTeamId = awayTeamId;

            matchesRepository.Add(match);

            return match.Id;
        }

        public Match Get(int key)
        {
            Match match = matchesRepository.All(x => x.Key == key).FirstOrDefault();

            return match;
        }

        public IEnumerable<Match> AllActive()
        {
            IEnumerable<Match> matches = matchesRepository.All(x => !x.IsDeleted);

            return matches;
        }

        public IEnumerable<Match> AllWithDeleted()
        {
            IEnumerable<Match> matches = matchesRepository.All(x => true);

            return matches;
        }

        public Match Update(string id, Match match)
        {
            Match matchToUpdate = matchesRepository.All(x => x.Id == id).FirstOrDefault();

            if (matchToUpdate != null)
            {
                matchToUpdate.Score = match.Score;
                matchToUpdate.Status = match.Status;
                matchToUpdate.StartTime = match.StartTime;
                matchToUpdate.StreamURL = match.StreamURL;

                matchesRepository.Update(matchToUpdate);
            }

            return matchToUpdate;
        }
    }
}