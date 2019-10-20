namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class AuthenticateAccountCommand : ICommand
    {
        public AuthenticateAccountCommand(string accountId, bool rememberMe)
        {
            AccountId = accountId;
            RememberMe = rememberMe;
        }

        public string AccountId { get; set; }

        public bool RememberMe { get; set; }
    }
}