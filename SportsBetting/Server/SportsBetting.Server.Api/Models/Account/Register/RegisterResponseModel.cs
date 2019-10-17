namespace SportsBetting.Server.Api.Models.Account.Register
{
    using SportsBetting.Common.Infrastructure.Mapping;

    public class RegisterResponseModel : IMapFrom<Data.Models.Account>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public decimal Balance { get; set; }
    }
}