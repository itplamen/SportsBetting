namespace SportsBetting.Handlers.Commands.Bets.CommandHandlers
{
    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Bets.Commands;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Contracts;

    public class PlaceBetCommandHandler : ICommandHandler<PlaceBetCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;
        private readonly ICommandHandler<UpdateAccountCommand> updateAccountHandler;
        private readonly IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler;

        public PlaceBetCommandHandler(
            ISportsBettingDbContext dbContext, 
            ICommandHandler<UpdateAccountCommand> updateAccountHandler,
            IQueryHandler<AccountByUsernameQuery, Account> accountByUsernameHandler)
        {
            this.dbContext = dbContext;
            this.updateAccountHandler = updateAccountHandler;
            this.accountByUsernameHandler = accountByUsernameHandler;
        }

        public string Handle(PlaceBetCommand command)
        {
            Bet bet = Mapper.Map<Bet>(command);
            dbContext.GetCollection<Bet>().InsertOne(bet);

            AccountByUsernameQuery accountQuery = new AccountByUsernameQuery(command.Username);
            Account account = accountByUsernameHandler.Handle(accountQuery);

            account.Balance -= command.Stake;

            UpdateAccountCommand updateAccountCommand = Mapper.Map<UpdateAccountCommand>(account);
            updateAccountHandler.Handle(updateAccountCommand);

            return bet.Id;
        }
    }
}