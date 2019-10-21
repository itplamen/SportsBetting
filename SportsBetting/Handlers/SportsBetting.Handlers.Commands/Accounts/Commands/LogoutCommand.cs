namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class LogoutCommand : ICommand
    {
        public LogoutCommand(string loginToken)
        {
            LoginToken = loginToken;
        }

        public string LoginToken { get; set; }
    }
}