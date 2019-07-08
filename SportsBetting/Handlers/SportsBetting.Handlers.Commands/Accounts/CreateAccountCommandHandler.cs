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

        public CreateAccountCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateAccountCommand command)
        {
            Account account = Mapper.Map<Account>(command);
            account.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Account>().InsertOne(account);

            return account.Id;
        }
    }
}