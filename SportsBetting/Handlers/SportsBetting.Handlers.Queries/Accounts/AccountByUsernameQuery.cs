namespace SportsBetting.Handlers.Queries.Accounts
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByUsernameQuery : IQuery<Account>
    {
        public AccountByUsernameQuery(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}