namespace SportsBetting.Feeder.Models
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models.Base;

    public class MarketFeedModel : BaseFeedModel
    {
        public MarketFeedModel()
        {
            Odds = new List<OddFeedModel>();
        }

        public string Name { get; set; }

        public int MatchId { get; set; }

        public IEnumerable<OddFeedModel> Odds { get; set; }

        protected override int GenerateId()
        {
            return Math.Abs(Name.GetHashCode() ^ MatchId.GetHashCode());
        }
    }
}