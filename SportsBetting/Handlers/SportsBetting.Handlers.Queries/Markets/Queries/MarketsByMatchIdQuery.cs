namespace SportsBetting.Handlers.Queries.Markets.Queries
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    using System.Collections.Generic;

    public class MarketsByMatchIdQuery : IQuery<IEnumerable<Market>>
    {
        public MarketsByMatchIdQuery(string matchId)
        {
            MatchId = matchId;
        }

        public string MatchId { get; set; }
    }
}