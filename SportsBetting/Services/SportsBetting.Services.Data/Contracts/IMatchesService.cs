namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IMatchesService
    {
        string Add(Match match, string categoryId, string tournamentId, string homeTeamId, string awayTeamId);

        Match Get(int key);

        Match Update(string id, Match match);
    }
}