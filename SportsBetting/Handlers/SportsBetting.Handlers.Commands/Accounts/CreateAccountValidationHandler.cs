namespace SportsBetting.Handlers.Commands.Accounts
{
    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CreateAccountValidationHandler : IValidationHandler<CreateAccountCommand>
    {
        private readonly IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler;

        public CreateAccountValidationHandler(IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler)
        {
            this.accountByExpressionHandler = accountByExpressionHandler;
        }

        public ValidationResult Validate(CreateAccountCommand command)
        {
            AccountByExpressionQuery byUsernameQuery = new AccountByExpressionQuery(x => x.Username == command.Username);
            Account accountByUsername = accountByExpressionHandler.Handle(byUsernameQuery);

            if (accountByUsername != null)
            {
                return GetErrorResult(nameof(command.Username));
            }

            AccountByExpressionQuery byEmailQuery = new AccountByExpressionQuery(x => x.Email == command.Email);
            Account accountByEmail = accountByExpressionHandler.Handle(byEmailQuery);

            if (accountByEmail != null)
            {
                return GetErrorResult(nameof(command.Email));
            }

            return new ValidationResult();
        }

        private ValidationResult GetErrorResult(string key)
        {
            return new ValidationResult(key, $"A user with the same {key.ToLower()} has already been registered!");
        }
    }
}