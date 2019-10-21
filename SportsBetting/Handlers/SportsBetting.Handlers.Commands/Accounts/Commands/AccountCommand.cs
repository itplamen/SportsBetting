namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    public class AccountCommand : PasswordCommand
    {
        public AccountCommand(string username, string password)
            : base(password)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}