namespace SportsBetting.Handlers.Commands.Accounts.ValidationHandlers
{
    using System.Collections.Generic;

    using SportsBetting.Common.Constants;
    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanCreateAccountValidationHandler : IValidationHandler<CreateAccountCommand>
    {
        private readonly IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler;

        public CanCreateAccountValidationHandler(IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler)
        {
            this.accountByExpressionHandler = accountByExpressionHandler;
        }

        public IEnumerable<ValidationResult> Validate(CreateAccountCommand command)
        {
            AccountByExpressionQuery byUsernameQuery = new AccountByExpressionQuery(x => x.Username == command.Username);
            Account accountByUsername = accountByExpressionHandler.Handle(byUsernameQuery);

            if (accountByUsername != null)
            {
                yield return new ValidationResult(nameof(command.Username), string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Username).ToLower()));
            }

            AccountByExpressionQuery byEmailQuery = new AccountByExpressionQuery(x => x.Email == command.Email);
            Account accountByEmail = accountByExpressionHandler.Handle(byEmailQuery);

            if (accountByEmail != null)
            {
                yield return new ValidationResult(nameof(command.Email), string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Email).ToLower()));
            }
        }
    }
}