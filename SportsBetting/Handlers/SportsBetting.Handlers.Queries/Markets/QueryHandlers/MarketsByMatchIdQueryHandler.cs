namespace SportsBetting.Handlers.Queries.Markets.QueryHandlers
{
    using System.Collections.Generic;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets.Queries;

    public class MarketsByMatchIdQueryHandler : IQueryHandler<MarketsByMatchIdQuery, IEnumerable<Market>>
    {
        private readonly ICache<Market> marketsCache;

        public MarketsByMatchIdQueryHandler(ICache<Market> marketsCache)
        {
            this.marketsCache = marketsCache;
        }

        public IEnumerable<Market> Handle(MarketsByMatchIdQuery query)
        {
            IEnumerable<Market> markets = marketsCache.All(x => x.MatchId == query.MatchId);

            return markets;
        }
    }
}