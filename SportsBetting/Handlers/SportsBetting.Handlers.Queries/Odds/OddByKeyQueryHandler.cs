namespace SportsBetting.Handlers.Queries.Odds
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OddByKeyQueryHandler : IQueryHandler<OddByKeyQuery, Odd>
    {
        private readonly ICache<Odd> oddsCache;

        public OddByKeyQueryHandler(ICache<Odd> oddsCache)
        {
            this.oddsCache = oddsCache;
        }

        public Odd Handle(OddByKeyQuery query)
        {
            Odd odd = oddsCache.All(x => x.Key == query.Key).FirstOrDefault();

            return odd;
        }
    }
}