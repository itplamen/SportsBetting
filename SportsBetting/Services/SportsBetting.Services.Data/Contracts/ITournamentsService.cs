namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ITournamentsService
    {
        string Add(int key, string name, string categoryId);

        Tournament Get(string name, string categoryId);

        IEnumerable<Tournament> All();
    }
}