namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface ITournamentsManager
    {
        void Manage(TournamentFeedModel feedModel, string categoryId);
    }
}