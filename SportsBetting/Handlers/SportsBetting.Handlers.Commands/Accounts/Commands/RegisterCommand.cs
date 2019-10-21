namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class RegisterCommand : ICommand, IMapTo<Account>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}