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

    public class CanRegisterValidationHandler : IValidationHandler<AccountCommand>
    {
        private readonly IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler;

        public CanRegisterValidationHandler(IQueryHandler<AccountByExpressionQuery, Account> accountByExpressionHandler)
        {
            this.accountByExpressionHandler = accountByExpressionHandler;
        }

        public IEnumerable<ValidationResult> Validate(AccountCommand command)
        {
            AccountByExpressionQuery byUsernameQuery = new AccountByExpressionQuery(x => x.Username == command.Username);
            Account accountByUsername = accountByExpressionHandler.Handle(byUsernameQuery);

            if (accountByUsername != null)
            {
                yield return new ValidationResult(nameof(command.Username), string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Username).ToLower()));
            }
        }
    }
}