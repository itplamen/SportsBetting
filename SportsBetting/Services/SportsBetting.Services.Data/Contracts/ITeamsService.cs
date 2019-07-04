namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ITeamsService
    {
        string Add(int key, string name, string sportId);

        Team Get(int key);

        IEnumerable<Team> Get(IEnumerable<string> teamIds);

        IEnumerable<Team> AllWithDeleted();
    }
}