namespace SportsBetting.Services.Feeder.Contracts.Factories
{
    using System;

    using SportsBetting.Feeder.Models;

    public interface IObjectFactory
    {
        MarketFeedModel CreateMarket(string name, int matchId);

        MatchFeedModel CreateMatch(string url, bool isLive, DateTime startTime, MatchFeedStatus status, TeamFeedModel homeTeam, TeamFeedModel awayTeam, TournamentFeedModel tournament);

        OddFeedModel CreateOdd(string name, decimal value, bool isSuspended, OddResultFeedStatus resultStatus, int rank, int matketId, string header = null);

        TeamFeedModel CreateTeam(string name, int? score);

        TournamentFeedModel CreateTournament(string name, string category);
    }
}
