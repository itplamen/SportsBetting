namespace SportsBetting.Services.Data
{
    using System.Collections.Generic;

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
    }
}