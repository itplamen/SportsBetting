namespace SportsBetting.Handlers.Queries.Accounts
{
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByEmailQueryHandler : IQueryHandler<AccountByEmailQuery, Account>
    {
        private readonly ISportsBettingDbContext dbContext;

        public AccountByEmailQueryHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Account Handle(AccountByEmailQuery query)
        {
            Account account = dbContext.GetCollection<Account>()
                .Find(x => x.Email == query.Email)
                .FirstOrDefault();

            return account;
        }
    }
}