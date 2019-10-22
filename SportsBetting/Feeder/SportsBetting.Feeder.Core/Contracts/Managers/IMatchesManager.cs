namespace SportsBetting.Feeder.Core.Contracts.Managers
{
    using SportsBetting.Feeder.Models;

    public interface IMatchesManager
    {
        string Manage(MatchFeedModel feedModel, string tournamentId, string homeTeamId, string awayTeamId);
    }
}