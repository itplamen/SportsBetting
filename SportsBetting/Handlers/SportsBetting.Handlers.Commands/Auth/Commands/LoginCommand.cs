namespace SportsBetting.Handlers.Commands.Auth.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class LoginCommand : ICommand
    {
        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}