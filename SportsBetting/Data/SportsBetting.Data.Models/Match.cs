namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Match : BaseModel
    {
        public string Score { get; set; }

        public string StreamURL { get; set; }

        public string CategoryId { get; set; }

        public string TournamentId { get; set; }

        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }
    }
}