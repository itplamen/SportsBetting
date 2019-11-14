namespace SportsBetting.Data.Models
{
    using SportsBetting.Data.Models.Base;

    public class Bet : BaseModel
    {
        public decimal Stake { get; set; }

        public string OddId { get; set; }

        public string Username { get; set; }
    }
}