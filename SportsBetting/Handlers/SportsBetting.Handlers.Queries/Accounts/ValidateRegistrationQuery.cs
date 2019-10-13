namespace SportsBetting.Handlers.Queries.Accounts
{
    using SportsBetting.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;

    public class ValidateRegistrationQuery : IQuery<ValidationResult>
    {
        public ValidateRegistrationQuery(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}