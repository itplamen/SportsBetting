namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchByKeyQueryHandler : IQueryHandler<MatchByKeyQuery, Match>
    {
        private readonly ICache<Match> matchesCache;

        public MatchByKeyQueryHandler(ICache<Match> matchesCache)
        {
            this.matchesCache = matchesCache;
        }

        public Match Handle(MatchByKeyQuery query)
        {
            Match match = matchesCache.All(x => x.Key == query.Key).FirstOrDefault();

            return match;
        }
    }
}