namespace SportsBetting.Clients.Web.Models.Home
{
    using System;

    using SportsBetting.Clients.Web.Mapping;
    using SportsBetting.Data.Models;

    public class GameViewModel : IMapFrom<Match>
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