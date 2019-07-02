namespace SportsBetting.Data.Models
{
    using System;

    using SportsBetting.Data.Models.Base;

    public class Match : BaseModel
    {
        public DateTime StartTime { get; set; }

        public MatchStatus Status { get; set; }

        public string Score { get; set; }

        public string StreamURL { get; set; }

        public string CategoryId { get; set; }

        public string TournamentId { get; set; }

        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }
    }
}