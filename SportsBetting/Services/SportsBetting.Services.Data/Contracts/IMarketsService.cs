namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IMarketsService
    {
        string Add(int key, string name, string matchId);

        Market Get(int key);
    }
}