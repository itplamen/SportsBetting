namespace SportsBetting.Handlers.Queries.Common.Results
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class OddResult : IMapFrom<Odd>
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Header { get; set; }

        public string Symbol { get; set; }

        public bool IsSuspended { get; set; }

        public int Rank { get; set; }
    }
}