namespace SportsBetting.Feeder.Models
{
    using System;

    using SportsBetting.Feeder.Models.Base;

    public class OddFeedModel : BaseFeedModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public int Rank { get; set; }

        public bool IsSuspended { get; set; }

        public int MarketId { get; set; }

        public OddResultFeedStatus ResultStatus { get; set; }

        protected override int GenerateId()
        {
            int headerHash = 0;

            if (!string.IsNullOrWhiteSpace(Header))
            {
                headerHash = Header.GetHashCode();
            }

            return Math.Abs(Name.GetHashCode() ^ headerHash ^ MarketId.GetHashCode());
        }
    }
}