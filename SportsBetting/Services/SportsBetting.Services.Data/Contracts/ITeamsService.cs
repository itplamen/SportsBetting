namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ITeamsService
    {
        IEnumerable<Team> Get(IEnumerable<string> teamIds);

        IEnumerable<Team> AllWithDeleted();
    }
}