namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface ITeamsManager
    {
        string Manage(TeamFeedModel feedModel);
    }
}