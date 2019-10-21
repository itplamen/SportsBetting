namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    public class LoginCommand : AuthenticateCommand
    {
        public LoginCommand(string accountId, bool rememberMe)
            : base(accountId)
        {
            RememberMe = rememberMe;
        }

        public bool RememberMe { get; set; }
    }
}