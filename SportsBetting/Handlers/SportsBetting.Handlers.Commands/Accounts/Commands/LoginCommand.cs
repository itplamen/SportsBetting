namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class LoginCommand : ICommand
    {
        public LoginCommand(string accountId, bool rememberMe)
        {
            AccountId = accountId;
            RememberMe = rememberMe;
        }

        public string AccountId { get; set; }

        public bool RememberMe { get; set; }
    }
}