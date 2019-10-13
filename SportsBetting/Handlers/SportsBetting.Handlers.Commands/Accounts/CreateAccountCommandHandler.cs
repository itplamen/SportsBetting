namespace SportsBetting.Handlers.Commands.Accounts
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Results;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountResult>
    {
        private readonly ISportsBettingDbContext dbContext;
        private readonly ICommandHandler<EncryptPasswordCommand, string> encryptPasswordHandler;

        public CreateAccountCommandHandler(
            ISportsBettingDbContext dbContext,
            ICommandHandler<EncryptPasswordCommand, string> encryptPasswordHandler)
        {
            this.dbContext = dbContext;
            this.encryptPasswordHandler = encryptPasswordHandler;
        }

        public AccountResult Handle(CreateAccountCommand command)
        {
            EncryptPasswordCommand encryptPasswordCommand = new EncryptPasswordCommand(command.Password);
            string password = encryptPasswordHandler.Handle(encryptPasswordCommand);

            Account account = Mapper.Map<Account>(command);
            account.Password = password;
            account.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Account>().InsertOne(account);

            return new AccountResult(account);
        }   
    }
}