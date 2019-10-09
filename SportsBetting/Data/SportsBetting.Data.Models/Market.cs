namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Market : BaseModel
    {
        public string Name { get; set; }

        public string MatchId { get; set; }
    }
}