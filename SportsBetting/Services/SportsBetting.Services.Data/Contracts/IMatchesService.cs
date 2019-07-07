namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface IMatchesService
    {
        IEnumerable<Match> AllActive();

        IEnumerable<Match> AllWithDeleted();
    }
}