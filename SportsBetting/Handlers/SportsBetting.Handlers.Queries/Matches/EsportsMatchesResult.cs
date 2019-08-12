namespace SportsBetting.Handlers.Queries.Matches
{
    using System;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class EsportsMatchesResult : IMapFrom<Match>
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public string Score { get; set; }

        public string Category { get; set; }

        public string Tournament { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }
    }
}