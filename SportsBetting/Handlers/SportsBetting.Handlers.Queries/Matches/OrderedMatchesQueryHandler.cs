namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OrderedMatchesQueryHandler : IQueryHandler<IEnumerable<Match>>
    {
        private readonly ICache<Match> matchesCache;

        public OrderedMatchesQueryHandler(ICache<Match> matchesCache)
        {
            this.matchesCache = matchesCache;
        }

        public IEnumerable<Match> Handle()
        {
            IEnumerable<Match> matches = matchesCache.All(_ => true).OrderBy(x => x.StartTime);

            return matches;
        }
    }
}