namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface IAccountsService
    {
        string Add(Account account);

        Account GetByUsername(string username);

        Account GetByEmail(string email);
    }
}