namespace SportsBetting.Feeder.Models
{
    using SportsBetting.Feeder.Models.Base;

    public class OddFeedModel : BaseFeedModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public int Rank { get; set; }

        public bool IsSuspended { get; set; }

        public int MarketKey { get; set; }

        public OddFeedType Type { get; set; }

        public OddResultFeedStatus ResultStatus { get; set; }

        protected override int GenerateKey()
        {
            int headerHash = 0;

            if (!string.IsNullOrWhiteSpace(Header))
            {
                headerHash = Header.GetHashCode();
            }

            return Name.GetHashCode() ^ headerHash ^ MarketKey.GetHashCode();
        }
    }
}