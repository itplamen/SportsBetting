namespace SportsBetting.Handlers.Commands.Auth.CommandHandlers
{
    using System;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class LoginCommandHandler : ICommandHandler<AuthCommand, Authentication>
    {
        private const int EXPIRATION_TIME = 30;

        private readonly ISportsBettingDbContext dbContext;

        public LoginCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Authentication Handle(AuthCommand command)
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