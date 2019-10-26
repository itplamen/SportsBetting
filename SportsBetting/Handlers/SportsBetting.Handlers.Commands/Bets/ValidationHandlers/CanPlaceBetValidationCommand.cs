namespace SportsBetting.Handlers.Commands.Bets.ValidationHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanPlaceBetValidationCommand : IValidationHandler<PlaceBetCommand>
    {
        private readonly IQueryHandler<EntitiesByIdQuery<Account>, IEnumerable<Account>> accountByIdHandler;

        public CanPlaceBetValidationCommand(IQueryHandler<EntitiesByIdQuery<Account>, IEnumerable<Account>> accountByIdHandler)
        {
            this.accountByIdHandler = accountByIdHandler;
        }

        public IEnumerable<ValidationResult> Validate(PlaceBetCommand command)
        {
            IEnumerable<string> accountId = new List<string>() { command.AccountId };
            EntitiesByIdQuery<Account> accountQuery = new EntitiesByIdQuery<Account>(accountId);

            Account account = accountByIdHandler.Handle(accountQuery).FirstOrDefault();

            if (account == null)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.AccountId), "Could not find account with such Id!")
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