namespace SportsBetting.Handlers.Queries.Accounts
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByEmailQuery : IQuery<Account>
    {
        public AccountByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}