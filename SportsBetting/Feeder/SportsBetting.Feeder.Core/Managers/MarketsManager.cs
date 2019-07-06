namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Commands.Markets;
    using SportsBetting.Services.Data.Contracts;

    public class MarketsManager : IMarketsManager
    {
        private readonly IMarketsService marketsService;
        private readonly ICommandHandler<CreateMarketCommand, string> createMarketHandler;

        public MarketsManager(IMarketsService marketsService, ICommandHandler<CreateMarketCommand, string> createMarketHandler)
        {
            this.marketsService = marketsService;
            this.createMarketHandler = createMarketHandler;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            Market market = marketsService.Get(feedModel.Id);

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