namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class UpdateAccountCommand : ICommand, IMapFrom<Account>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public bool IsVerified { get; set; }
    }
}