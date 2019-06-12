namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IOddsService
    {
        string Add(Odd odd, string marketId, string matchId);

        Odd Get(int key);

        Odd Update(string id, Odd odd);
    }
}