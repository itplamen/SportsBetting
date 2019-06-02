namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface ITournamentsManager
    {
        string Manage(TournamentFeedModel feedModel, string categoryId);
    }
}