namespace SportsBetting.Handlers.Queries.Odds.Queries
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OddsByMarketIdQuery : IQuery<IEnumerable<Odd>>
    {
        public OddsByMarketIdQuery(string marketId)
        {
            MarketId = marketId;
        }

        public string MarketId { get; set; }
    }
}