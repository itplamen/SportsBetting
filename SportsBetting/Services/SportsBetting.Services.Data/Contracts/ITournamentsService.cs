namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ITournamentsService
    {
        Tournament Get(string name, string categoryId);

        IEnumerable<Tournament> Get(IEnumerable<string> tournamentIds);

        IEnumerable<Tournament> AllWithDeleted();
    }
}