namespace SportsBetting.Handlers.Queries.Accounts
{
    using SportsBetting.Common.Validation;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountValidationQuery : IQuery<ValidationResult>
    {
        public AccountValidationQuery(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}