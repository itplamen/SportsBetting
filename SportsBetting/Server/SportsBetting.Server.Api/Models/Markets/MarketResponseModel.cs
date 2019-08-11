namespace SportsBetting.Server.Api.Models.Markets
{
    using System.Collections.Generic;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Server.Api.Models.Odds;

    public class MarketResponseModel : IMapFrom<Market>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<OddResponseModel> Odds { get; set; }
    }
}