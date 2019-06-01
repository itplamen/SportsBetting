namespace SportsBetting.Feeder.Core.Managers
{
    using System.Collections.Generic;
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

        public void Manage(IEnumerable<MarketFeedModel> feedModels, string matchId)
        {
            foreach (var feedModel in feedModels)
            {
                Market market = marketsRepository.All(x => x.Key == feedModel.Id).FirstOrDefault();

                if (market == null)
                {
                    Add(feedModel, matchId);
                }
            }
        }

        private void Add(MarketFeedModel feedModel, string matchId)
        {
            Market market = new Market()
            {
                Key = feedModel.Id,
                Name = feedModel.Name,
                MatchId = matchId
            };

            marketsRepository.Add(market);
        }
    }
}