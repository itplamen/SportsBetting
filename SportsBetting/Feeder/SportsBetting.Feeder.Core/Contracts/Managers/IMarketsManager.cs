namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface IMarketsManager
    {
        string Manage(MarketFeedModel feedModel, string matchId);
    }
}