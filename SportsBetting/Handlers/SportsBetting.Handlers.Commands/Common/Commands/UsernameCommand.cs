namespace SportsBetting.Handlers.Commands.Common.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class UsernameCommand : ICommand
    {
        public UsernameCommand(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}