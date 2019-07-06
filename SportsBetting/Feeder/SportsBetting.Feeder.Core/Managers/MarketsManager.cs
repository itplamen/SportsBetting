namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Markets;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets;

    public class MarketsManager : IMarketsManager
    {
        private readonly IQueryHandler<MarketByKeyQuery, Market> getMarketByKeyHandler;
        private readonly ICommandHandler<CreateMarketCommand, string> createMarketHandler;

        public MarketsManager(
            IQueryHandler<MarketByKeyQuery, Market> getMarketByKeyHandler, 
            ICommandHandler<CreateMarketCommand, string> createMarketHandler)
        {
            this.getMarketByKeyHandler = getMarketByKeyHandler;
            this.createMarketHandler = createMarketHandler;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            MarketByKeyQuery query = new MarketByKeyQuery()
            {
                Key = feedModel.Id
            };

            Market market = getMarketByKeyHandler.Handle(query);

            if (market != null)
            {
                return market.Id;
            }

            CreateMarketCommand command = new CreateMarketCommand()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                MatchId = matchId
            };

            return createMarketHandler.Handle(command);
        }
    }
}