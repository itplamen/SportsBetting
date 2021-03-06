﻿namespace SportsBetting.Handlers.Commands.Auth.ValidationHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class CanLoginValidationHandler : IValidationHandler<AccountCommand>
    {
        private readonly ICommandHandler<PasswordCommand, string> encryptPasswordHandler;
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountHandler;

        public CanLoginValidationHandler(
            ICommandHandler<PasswordCommand, string> encryptPasswordHandler, 
            IQueryHandler<AccountByUsernameQuery, Account> accountHandler)
        {
            this.encryptPasswordHandler = encryptPasswordHandler;
            this.accountHandler = accountHandler;
        }

        public IEnumerable<ValidationResult> Validate(AccountCommand command)
        {
            AccountByUsernameQuery query = new AccountByUsernameQuery(command.Username);
            Account account = accountHandler.Handle(query);

            if (account == null)
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult(nameof(command.Username), "Could not find account with such username!")
                };
            }

            PasswordCommand encryptPasswordCommand = new PasswordCommand(command.Password);
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