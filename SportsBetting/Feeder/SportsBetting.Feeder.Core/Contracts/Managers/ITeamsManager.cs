namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface ITeamsManager
    {
        void Manage(TeamFeedModel feedModel);
    }
}