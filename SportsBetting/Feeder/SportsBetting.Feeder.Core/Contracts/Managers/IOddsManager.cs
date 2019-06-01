namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models;
    
    public interface IOddsManager
    {
        void Manage(IEnumerable<OddFeedModel> feedModels, string marketId, string matchId);
    }
}