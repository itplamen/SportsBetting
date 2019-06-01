namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models;

    public interface IMarketsManager
    {
        void Manage(IEnumerable<MarketFeedModel> feedModels, string matchId);
    }
}