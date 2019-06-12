namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Data.Contracts;

    public class MarketsManager : IMarketsManager
    {
        private readonly IMarketsService marketsService;

        public MarketsManager(IMarketsService marketsService)
        {
            this.marketsService = marketsService;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            Market market = marketsService.Get(feedModel.Id);

            if (market != null)
            {
                return market.Id;
            }

            return marketsService.Add(feedModel.Id, feedModel.Name, matchId);
        }
    }
}