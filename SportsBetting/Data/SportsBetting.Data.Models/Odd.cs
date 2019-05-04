namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Odd : BaseModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }

        public int Rank { get; set; }

        public OddResultStatus ResultStatus { get; set; }

        public string MarketId { get; set; }

        public string MatchId { get; set; }
    }
}