namespace SportsBetting.Feeder.Core.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models;

    public interface IUnprocessedFeedManager
    {
        void Manage(IEnumerable<MatchFeedModel> processedFeed);
    }
}