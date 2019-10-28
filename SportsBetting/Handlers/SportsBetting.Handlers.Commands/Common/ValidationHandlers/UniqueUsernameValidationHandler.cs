namespace SportsBetting.Handlers.Commands.Common.ValidationHandlers
{
    using System.Collections.Generic;

    using SportsBetting.Common.Constants;
    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class UniqueUsernameValidationHandler : IValidationHandler<UsernameCommand>
    {
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountHandler;

        public UniqueUsernameValidationHandler(IQueryHandler<AccountByUsernameQuery, Account> accountHandler)
        {
            this.accountHandler = accountHandler;
        }

        public IEnumerable<ValidationResult> Validate(UsernameCommand command)
        {
            AccountByUsernameQuery query = new AccountByUsernameQuery(command.Username);
            Account account = accountHandler.Handle(query);

            if (account != null)
            {
                yield return new ValidationResult(
                    nameof(command.Username), 
                    string.Format(MessageConstants.ALREADY_REGISTERED, nameof(command.Username).ToLower()));
            }
        }
    }
}