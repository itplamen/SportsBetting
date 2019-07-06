namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ITournamentsService
    {
        IEnumerable<Tournament> Get(IEnumerable<string> tournamentIds);

        IEnumerable<Tournament> AllWithDeleted();
    }
}