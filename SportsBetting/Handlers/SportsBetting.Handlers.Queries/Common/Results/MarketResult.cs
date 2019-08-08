namespace SportsBetting.Handlers.Queries.Common.Results
{
    using System.Collections.Generic;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class MarketResult : IMapFrom<Market>
    {
        public string Name { get; set; }

        public IEnumerable<OddResult> Odds { get; set; }
    }
}