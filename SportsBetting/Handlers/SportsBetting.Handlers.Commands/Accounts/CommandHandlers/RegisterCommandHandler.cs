namespace SportsBetting.Handlers.Commands.Accounts.CommandHandlers
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class RegisterCommandHandler : ICommandHandler<AccountCommand, Account>
    {
        private readonly ISportsBettingDbContext dbContext;
        private readonly ICommandHandler<PasswordCommand, string> encryptPasswordHandler;

        public RegisterCommandHandler(
            ISportsBettingDbContext dbContext,
            ICommandHandler<PasswordCommand, string> encryptPasswordHandler)
        {
            this.dbContext = dbContext;
            this.encryptPasswordHandler = encryptPasswordHandler;
        }

        public Account Handle(AccountCommand command)
        {
            PasswordCommand encryptPasswordCommand = new PasswordCommand(command.Password);
            string password = encryptPasswordHandler.Handle(encryptPasswordCommand);

            Account account = Mapper.Map<Account>(command);
            account.Password = password;
            account.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Account>().InsertOne(account);

            return account;
        }   
    }
}