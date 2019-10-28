namespace SportsBetting.Handlers.Commands.Common.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class PasswordCommand : ICommand
    {
        public PasswordCommand(string password)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}