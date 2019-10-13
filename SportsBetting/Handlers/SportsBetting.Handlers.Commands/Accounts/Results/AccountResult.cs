namespace SportsBetting.Handlers.Commands.Accounts.Results
{
    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;

    public class AccountResult : ValidationResult
    {
        public AccountResult(Account account)
        {
            Account = account;
        }

        public Account Account { get; set; }
    }
}