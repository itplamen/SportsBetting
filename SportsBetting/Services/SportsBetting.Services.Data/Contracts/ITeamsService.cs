namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface ITeamsService
    {
        string Add(int key, string name, string sportId);

        Team Get(int key);
    }
}