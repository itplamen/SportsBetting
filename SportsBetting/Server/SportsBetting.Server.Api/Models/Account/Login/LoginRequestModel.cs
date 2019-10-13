namespace SportsBetting.Server.Api.Models.Account.Login
{
    public class LoginRequestModel : AccountRequestModel
    {
        public bool RememberMe { get; set; }
    }
}