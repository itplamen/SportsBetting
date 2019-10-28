namespace SportsBetting.Handlers.Queries.Accounts
{
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByUsernameQueryHandler : IQueryHandler<AccountByUsernameQuery, Account>
    {
        private readonly ISportsBettingDbContext dbContext;

        public AccountByUsernameQueryHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Account Handle(AccountByUsernameQuery query)
        {
            Account account = dbContext.GetCollection<Account>()
                .Find(x => x.Username == query.Username)
                .FirstOrDefault();

            return account;
        }
    }
}