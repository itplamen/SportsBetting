namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IMarketsService
    {
        Market Get(int key);
    }
}