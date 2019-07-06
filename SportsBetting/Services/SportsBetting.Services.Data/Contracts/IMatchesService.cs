namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface IMatchesService
    {
        string Add(Match match, string categoryId, string tournamentId, string homeTeamId, string awayTeamId);

        IEnumerable<Match> AllActive();

        IEnumerable<Match> AllWithDeleted();

        Match Update(string id, Match match);
    }
}