namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IOddsService
    {
        Odd Update(string id, Odd odd);
    }
}