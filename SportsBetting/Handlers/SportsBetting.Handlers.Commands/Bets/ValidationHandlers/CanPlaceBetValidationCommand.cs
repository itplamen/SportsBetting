namespace SportsBetting.Handlers.Commands.Bets.ValidationHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanPlaceBetValidationCommand : IValidationHandler<PlaceBetCommand>
    {
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler;

        public CanPlaceBetValidationCommand(IQueryHandler<AccountByUsernameQuery, Account> accountByIdHandler)
        {
            this.accountByUsernameHandler = accountByIdHandler;
        }

        public IEnumerable<ValidationResult> Validate(PlaceBetCommand command)
        {
            AccountByUsernameQuery accountQuery = new AccountByUsernameQuery(command.Username);
            Account account = accountByUsernameHandler.Handle(accountQuery);

            if (account == null)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Username), "Could not find account with such username!")
                };
            }

            if (account.Balance - command.Stake < 0)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Stake), "User does not have enough balance!")
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}