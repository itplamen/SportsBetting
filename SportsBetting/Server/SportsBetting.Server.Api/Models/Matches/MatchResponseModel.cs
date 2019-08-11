namespace SportsBetting.Server.Api.Models.Matches
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Server.Api.Models.Markets;

    public class MatchResponseModel : IMapFrom<Match>
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public string Score { get; set; }

        public string Category { get; set; }

        public string Tournament { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public IEnumerable<MarketResponseModel> Markets { get; set; }
    }
}