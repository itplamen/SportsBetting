namespace SportsBetting.Handlers.Queries.Markets
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MarketByKeyQueryHandler : IQueryHandler<MarketByKeyQuery, Market>
    {
        private readonly ICache<Market> marketsCache;

        public MarketByKeyQueryHandler(ICache<Market> marketsCache)
        {
            this.marketsCache = marketsCache;
        }

        public Market Handle(MarketByKeyQuery query)
        {
            Market market = marketsCache.All(x => x.Key == query.Key).FirstOrDefault();

            return market;
        }
    }
}