namespace SportsBetting.Handlers.Queries.Markets
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MarketByKeyQuery : IQuery<Market>
    {
        public int Key { get; set; }
    }
}