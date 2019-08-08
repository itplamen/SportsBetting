namespace SportsBetting.Handlers.Queries.Common.Results
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class OddResult : IMapFrom<Odd>
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public bool IsSuspended { get; set; }
    }
}