namespace SportsBetting.Handlers.Queries.Accounts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MongoDB.Driver;

    using SportsBetting.Common.Validation;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountValidationQueryHandler : IQueryHandler<AccountValidationQuery, ValidationResult>
    {
        private const string ERROR_MESSAGE = "A user with the same {0} has already been registered!";

        private readonly ISportsBettingDbContext dbContext;

        public AccountValidationQueryHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ValidationResult Handle(AccountValidationQuery query)
        {
            if (GetAccount(x => x.Username == query.Username) != null)
            {
                return GetErrorResult(nameof(query.Username));
            }

            if (GetAccount(x => x.Email == query.Email) != null)
            {
                return GetErrorResult(nameof(query.Email));
            }

            return new ValidationResult();
        }

        private Account GetAccount(Expression<Func<Account, bool>> expression)
        {
            return dbContext.GetCollection<Account>()
                .Find(expression)
                .FirstOrDefault();
        }

        private ValidationResult GetErrorResult(string key)
        {
            return new ValidationResult(key, $"A user with the same {key.ToLower()} has already been registered!");
        }
    }
}