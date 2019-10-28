namespace SportsBetting.Handlers.Commands.Bets.CommandHandlers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common.Queries;
    using SportsBetting.Handlers.Queries.Contracts;

    public class PlaceBetCommandHandler : ICommandHandler<PlaceBetCommand>
    {
        private readonly ISportsBettingDbContext dbContext;
        private readonly ICommandHandler<UpdateAccountCommand> updateAccountHandler;
        private readonly IQueryHandler<EntitiesByIdQuery<Account>, IEnumerable<Account>> getAccountByIdHandler;

        public PlaceBetCommandHandler(
            ISportsBettingDbContext dbContext, 
            ICommandHandler<UpdateAccountCommand> updateAccountHandler,
            IQueryHandler<EntitiesByIdQuery<Account>, IEnumerable<Account>> getAccountByIdHandler)
        {
            this.dbContext = dbContext;
            this.updateAccountHandler = updateAccountHandler;
            this.getAccountByIdHandler = getAccountByIdHandler;
        }

        public void Handle(PlaceBetCommand command)
        {
            Bet bet = Mapper.Map<Bet>(command);
            dbContext.GetCollection<Bet>().InsertOne(bet);

            IEnumerable<string> accountId = new List<string>() { command.AccountId };
            EntitiesByIdQuery<Account> accountQuery = new EntitiesByIdQuery<Account>(accountId);

            Account account = getAccountByIdHandler.Handle(accountQuery).FirstOrDefault();
            account.Balance -= command.Stake;

            UpdateAccountCommand updateAccountCommand = Mapper.Map<UpdateAccountCommand>(account);
            updateAccountHandler.Handle(updateAccountCommand);
        }
    }
}