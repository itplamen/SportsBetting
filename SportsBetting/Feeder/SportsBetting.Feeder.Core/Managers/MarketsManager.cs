namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Markets;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MarketsManager : IMarketsManager
    {
        private readonly ICommandHandler<CreateMarketCommand, string> createMarketHandler;
        private readonly IQueryHandler<EntityByKeyQuery<Market>, Market> marketByKeyHandler;

        public MarketsManager(
            ICommandHandler<CreateMarketCommand, string> createMarketHandler,
            IQueryHandler<EntityByKeyQuery<Market>, Market> marketByKeyHandler)
        {
            this.createMarketHandler = createMarketHandler;
            this.marketByKeyHandler = marketByKeyHandler;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            EntityByKeyQuery<Market> query = new EntityByKeyQuery<Market>(feedModel.Id);
            Market market = marketByKeyHandler.Handle(query);

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