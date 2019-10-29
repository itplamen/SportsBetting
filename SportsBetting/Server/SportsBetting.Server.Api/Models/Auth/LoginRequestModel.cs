namespace SportsBetting.Server.Api.Models.Auth
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Common.Commands;
    using SportsBetting.Server.Api.Models.Common;

    public class LoginRequestModel : AccountRequestModel, IMapTo<AccountCommand>
    {
        public bool RememberMe { get; set; }
    }
}