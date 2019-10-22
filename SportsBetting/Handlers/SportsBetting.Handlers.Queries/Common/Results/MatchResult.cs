namespace SportsBetting.Handlers.Queries.Common.Results
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    
    public class MatchResult : IMapFrom<Match>
    {
        public string Id { get; set; }

        public string Score { get; set; }

        public MatchType Type { get; set; }

        public DateTime StartTime { get; set; }

        public string Category { get; set; }

        public string Tournament { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public IEnumerable<MarketResult> Markets { get; set; }
    }
}