namespace SportsBetting.Handlers.Commands.Auth.Commands
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class LogoutCommand : ICommand
    {
        public LogoutCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}