namespace SportsBetting.Feeder.Models
{
    using SportsBetting.Feeder.Models.Base;

    public class OddFeedModel : BaseFeedModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Header { get; set; }

        public string Symbol { get; set; }

        public int Rank { get; set; }

        public bool IsSuspended { get; set; }

        public OddResultFeedStatus ResultStatus { get; set; }

        protected override int GenerateKey()
        {
            return Name.GetHashCode() ^ Header.GetHashCode() ^ Rank.GetHashCode();
        }
    }
}