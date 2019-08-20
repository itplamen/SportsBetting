namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models;

    public interface IUnprocessedFeedManager
    {
        void Manage(IEnumerable<MatchFeedModel> processedFeed);
    }
}