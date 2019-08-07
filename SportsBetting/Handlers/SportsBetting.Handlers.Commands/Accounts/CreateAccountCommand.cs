namespace SportsBetting.Handlers.Commands.Accounts
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateAccountCommand : ICommand, IMapTo<Account>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public AccontRole Role { get; set; }
    }
}