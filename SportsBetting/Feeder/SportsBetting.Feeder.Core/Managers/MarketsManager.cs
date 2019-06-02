namespace SportsBetting.Feeder.Core.Managers
{
    using System.Linq;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;

    public class MarketsManager : IMarketsManager
    {
        private readonly IRepository<Market> marketsRepository;

        public MarketsManager(IRepository<Market> marketsRepository)
        {
            this.marketsRepository = marketsRepository;
        }

        public string Manage(MarketFeedModel feedModel, string matchId)
        {
            Market market = marketsRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();

            if (market == null)
            {
                return Add(feedModel, matchId);
            }

            return market.Id;
        }

        private string Add(MarketFeedModel feedModel, string matchId)
        {
            Market market = new Market()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                MatchId = matchId
            };

            marketsRepository.Add(market);

            return market.Id;
        }
    }
}