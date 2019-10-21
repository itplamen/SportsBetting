namespace SportsBetting.Handlers.Commands.Accounts.Commands
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class RegisterCommand : AccountCommand, IMapTo<Account>
    {
        public RegisterCommand(string username, string password, string email) 
            : base(username, password)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}