namespace SportsBetting.Server.Api.Models.Account
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Accounts.Commands;

    public class LoginRequestModel : AccountRequestModel, IMapTo<LoginAccountCommand>
    {
        public bool RememberMe { get; set; }
    }
}