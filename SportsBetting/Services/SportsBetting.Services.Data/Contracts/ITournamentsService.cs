namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface ITournamentsService
    {
        string Add(int key, string name, string categoryId);

        Tournament Get(string name, string categoryId);
    }
}