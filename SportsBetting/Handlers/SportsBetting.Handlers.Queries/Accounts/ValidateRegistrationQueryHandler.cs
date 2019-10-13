namespace SportsBetting.Handlers.Queries.Accounts
{
    using SportsBetting.Common.Validation;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class ValidateRegistrationQueryHandler : IQueryHandler<ValidateRegistrationQuery, ValidationResult>
    {
        private const string ERROR_MESSAGE = "A user with the same {0} has already been registered!";

        private readonly IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler;

        public ValidateRegistrationQueryHandler(IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler)
        {
            this.accountByExpressionHandler = accountByExpressionHandler;
        }

        public ValidationResult Handle(ValidateRegistrationQuery query)
        {
            AccountByExpressionQuery byUsernameQuery = new AccountByExpressionQuery(x => x.Username == query.Username);
            Account accountByUsername = accountByExpressionHandler.Handle(byUsernameQuery);

            if (accountByUsername != null)
            {
                return GetErrorResult(nameof(query.Username));
            }

            AccountByExpressionQuery byEmailQuery = new AccountByExpressionQuery(x => x.Email == query.Email);
            Account accountByEmail = accountByExpressionHandler.Handle(byEmailQuery);

            if (accountByEmail != null)
            {
                return GetErrorResult(nameof(query.Email));
            }

            return new ValidationResult();
        }

        private ValidationResult GetErrorResult(string key)
        {
            return new ValidationResult(key, $"A user with the same {key.ToLower()} has already been registered!");
        }
    }
}