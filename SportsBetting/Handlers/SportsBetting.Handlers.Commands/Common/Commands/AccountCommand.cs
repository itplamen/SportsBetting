namespace SportsBetting.Handlers.Commands.Common.Commands
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class AccountCommand : PasswordCommand, IMapTo<Account>
    {
        public AccountCommand(string username, string password)
            : base(password)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}