namespace SportsBetting.Handlers.Commands.Accounts
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class EncryptPasswordCommand : ICommand
    {
        public EncryptPasswordCommand(string password)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}