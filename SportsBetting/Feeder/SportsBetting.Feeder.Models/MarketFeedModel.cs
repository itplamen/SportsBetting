namespace SportsBetting.Feeder.Models
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models.Base;

    public class MarketFeedModel : BaseFeedModel
    {
        public MarketFeedModel()
        {
            Odds = new List<OddFeedModel>();
        }

        public string Name { get; set; }

        public int MatchKey { get; set; }

        public IEnumerable<OddFeedModel> Odds { get; set; }

        protected override int GenerateKey()
        {
            return Name.GetHashCode() ^ MatchKey.GetHashCode();
        }
    }
}