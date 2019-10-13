namespace SportsBetting.Handlers.Queries.Accounts
{
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByExpressionQueryHandler : IQueryHandler<AccountByExpressionQuery, Account>
    {
        private readonly ISportsBettingDbContext dbContext;

        public AccountByExpressionQueryHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Account Handle(AccountByExpressionQuery query)
        {
            Account account = dbContext.GetCollection<Account>()
                .Find(query.Expression)
                .FirstOrDefault();

            return account;
        }
    }
}