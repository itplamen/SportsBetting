namespace SportsBetting.Server.Api.Models.Odds
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class OddResponseModel : IMapFrom<Odd>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public bool IsSuspended { get; set; }
    }
}