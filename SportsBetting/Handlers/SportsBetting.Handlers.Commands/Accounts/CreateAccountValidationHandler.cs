namespace SportsBetting.Handlers.Commands.Accounts
{
    using SportsBetting.Common.Constants;
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
                return new ValidationResult(nameof(command.Username), string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Username).ToLower()));
            }

            AccountByExpressionQuery byEmailQuery = new AccountByExpressionQuery(x => x.Email == command.Email);
            Account accountByEmail = accountByExpressionHandler.Handle(byEmailQuery);

            if (accountByEmail != null)
            {
                return new ValidationResult(nameof(command.Email), string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Email).ToLower()));
            }

            return new ValidationResult();
        }
    }
}