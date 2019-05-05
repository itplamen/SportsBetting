namespace SportsBetting.Services.Feeder.Contracts.Factories
{
    using System;

    using SportsBetting.Feeder.Models;

    public interface IObjectFactory
    {
        Market CreateMarket(string name, int matchId);

        Match CreateMatch(string url, bool isLive, DateTime startTime, MatchStatus status, Team homeTeam, Team awayTeam, Tournament tournament);

        Odd CreateOdd(string name, decimal value, bool isSuspended, OddResultStatus resultStatus, int rank, int matketId, string header = null);

        Team CreateTeam(string name, int? score);

        Tournament CreateTournament(string name, string category);
    }
}
