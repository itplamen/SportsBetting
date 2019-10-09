namespace SportsBetting.Server.Api.Models.Odds
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Queries.Common.Results;

    public class OddResponseModel : IMapFrom<OddResult>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Header { get; set; }

        public string Symbol { get; set; }

        public bool IsSuspended { get; set; }

        public int Rank { get; set; }
    }
}