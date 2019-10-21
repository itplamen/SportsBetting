namespace SportsBetting.Handlers.Commands.Accounts.CommandHandlers
{
    using System;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class AuthenticateAccountCommandHandler : ICommandHandler<LoginCommand, Authentication>
    {
        private const int EXPIRATION_TIME = 30;

        private readonly ISportsBettingDbContext dbContext;

        public AuthenticateAccountCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Authentication Handle(LoginCommand command)
        {
            Authentication authentication = new Authentication();
            authentication.AccountId = command.AccountId;
            authentication.Expiration = CalculateExpirationDate(command.RememberMe);

            dbContext.GetCollection<Authentication>().InsertOne(authentication);

            return authentication;
        }

        private DateTime CalculateExpirationDate(bool rememberMe)
        {
            if (rememberMe)
            {
                return DateTime.Now.AddDays(EXPIRATION_TIME);
            }

            return DateTime.Now.AddMinutes(EXPIRATION_TIME);
        }
    }
}