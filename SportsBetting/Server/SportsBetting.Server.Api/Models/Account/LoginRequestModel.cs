namespace SportsBetting.Server.Api.Models.Account
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Accounts.Commands;

    public class LoginRequestModel : AccountRequestModel, IMapTo<AccountCommand>
    {
        public bool RememberMe { get; set; }
    }
}