namespace SportsBetting.Clients.Web.Areas.Administration.Models.Matches
{
    using System;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Base;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;

    public class MatchViewModel : BaseViewModel, IMapFrom<Match>
    {
        public DateTime StartTime { get; set; }

        public string Status { get; set; }

        public string Score { get; set; }

        public string StreamURL { get; set; }

        public string CategoryId { get; set; }

        public string TournamentId { get; set; }

        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }
    }
}