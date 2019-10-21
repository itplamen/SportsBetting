namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class AuthenticateCommand : ICommand
    {
        public AuthenticateCommand(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; set; }
    }
}