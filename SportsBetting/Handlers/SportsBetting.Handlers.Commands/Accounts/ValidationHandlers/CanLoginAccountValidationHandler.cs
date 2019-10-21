namespace SportsBetting.Handlers.Commands.Accounts.ValidationHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanLoginAccountValidationHandler : IValidationHandler<LoginAccountCommand>
    {
        private readonly ICommandHandler<EncryptPasswordCommand, string> encryptPasswordHandler;
        private readonly IQueryHandler<AccountByExpressionQuery, Account> accountByUsernameHandler;

        public CanLoginAccountValidationHandler(
            ICommandHandler<EncryptPasswordCommand, string> encryptPasswordHandler, 
            IQueryHandler<AccountByExpressionQuery, Account> accountByUsernameHandler)
        {
            this.encryptPasswordHandler = encryptPasswordHandler;
            this.accountByUsernameHandler = accountByUsernameHandler;
        }

        public IEnumerable<ValidationResult> Validate(LoginAccountCommand command)
        {
            AccountByExpressionQuery query = new AccountByExpressionQuery(x => x.Username == command.Username);
            Account account = accountByUsernameHandler.Handle(query);

            if (account == null)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Username), "Could not find account with such username!")
                };
            }

            EncryptPasswordCommand encryptPasswordCommand = new EncryptPasswordCommand(command.Password);
            string encryptedPassword = encryptPasswordHandler.Handle(encryptPasswordCommand);

            if (encryptedPassword != account.Password)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Password), "Invalid password!")
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}