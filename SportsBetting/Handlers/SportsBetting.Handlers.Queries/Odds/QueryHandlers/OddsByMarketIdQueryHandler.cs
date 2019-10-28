namespace SportsBetting.Handlers.Queries.Odds.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Odds.Queries;

    public class OddsByMarketIdQueryHandler : IQueryHandler<OddsByMarketIdQuery, IEnumerable<Odd>>
    {
        private readonly ICache<Odd> oddsCache;

        public OddsByMarketIdQueryHandler(ICache<Odd> oddsCache)
        {
            this.oddsCache = oddsCache;
        }

        public IEnumerable<Odd> Handle(OddsByMarketIdQuery query)
        {
            IEnumerable<Odd> odds = oddsCache.All(x => x.MarketId == query.MarketId)
                .OrderBy(x => x.Header)
                .ThenBy(x => x.Rank);

            return odds;
        }
    }
}