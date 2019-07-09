namespace SportsBetting.Handlers.Commands.Markets
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateMarketCommand : ICommand, IMapFrom<MarketFeedModel>, IMapTo<Market>
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string MatchId { get; set; }
    }
}