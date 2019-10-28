namespace SportsBetting.Handlers.Commands.Accounts.CommandHandlers
{
    using System;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly ISportsBettingDbContext dbContext;

        public UpdateAccountCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle(UpdateAccountCommand command)
        {
            FilterDefinition<Account> filter = Builders<Account>.Filter.Eq(x => x.Id, command.Id);
            UpdateDefinition<Account> update = Builders<Account>.Update
               .Set(x => x.Username, command.Username)
               .Set(x => x.Password, command.Password)
               .Set(x => x.Balance, command.Balance)
               .Set(x => x.IsVerified, command.IsVerified)
               .Set(x => x.ModifiedOn, DateTime.UtcNow);

            dbContext.GetCollection<Account>().UpdateOne(filter, update);
        }
    }
}