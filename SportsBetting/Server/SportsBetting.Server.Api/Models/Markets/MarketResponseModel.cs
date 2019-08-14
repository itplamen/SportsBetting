namespace SportsBetting.Server.Api.Models.Markets
{
    using System.Collections.Generic;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Server.Api.Models.Odds;

    public class MarketResponseModel : IMapFrom<MarketResult>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<OddResponseModel> Odds { get; set; }
    }
}