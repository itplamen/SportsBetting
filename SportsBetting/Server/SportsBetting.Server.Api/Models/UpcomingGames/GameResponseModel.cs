namespace SportsBetting.Server.Api.Models.UpcomingGames
{
    using System;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Queries.Matches;

    public class GameResponseModel : IMapFrom<UpcomingMatchesResult>
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