namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface IFeedManager
    {
        void Manage(MatchFeedModel feedModel);
    }
}