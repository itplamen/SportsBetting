namespace SportsBetting.Clients.Web.Areas.Administration.Models.Accounts
{
    using SportsBetting.Clients.Web.Areas.Administration.Models.Base;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class AccountViewModel : BaseViewModel, IMapFrom<Account>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public string Role { get; set; }

        public bool IsVerified { get; set; }
    }
}