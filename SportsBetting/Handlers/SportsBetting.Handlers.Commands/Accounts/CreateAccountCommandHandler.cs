namespace SportsBetting.Handlers.Commands.Accounts
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, string>
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

        public string Handle(CreateAccountCommand command)
        {
            EncryptPasswordCommand encryptPasswordCommand = new EncryptPasswordCommand(command.Password);
            string password = encryptPasswordHandler.Handle(encryptPasswordCommand);

            Account account = Mapper.Map<Account>(command);
            account.Password = password;
            account.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Account>().InsertOne(account);

            return account.Id;
        }   
    }
}