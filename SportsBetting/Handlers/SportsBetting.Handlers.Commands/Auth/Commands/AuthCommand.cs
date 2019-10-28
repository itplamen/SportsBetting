namespace SportsBetting.Handlers.Commands.Auth.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class AuthCommand : ICommand
    {
        public AuthCommand(string accountId, bool rememberMe)
        {
            AccountId = accountId;
            RememberMe = rememberMe;
        }

        public string AccountId { get; set; }

        public bool RememberMe { get; set; }
    }
}