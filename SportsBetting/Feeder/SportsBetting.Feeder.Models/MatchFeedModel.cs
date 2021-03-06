﻿namespace SportsBetting.Feeder.Models
{
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models.Base;

    public class MatchFeedModel : BaseFeedModel
    {
        public MatchFeedModel()
        {
            Markets = new List<MarketFeedModel>();
        }

        public MatchFeedType Type { get; set; }

        public TeamFeedModel HomeTeam { get; set; }

        public TeamFeedModel AwayTeam { get; set; }

        public TournamentFeedModel Tournament { get; set; }

        public IEnumerable<MarketFeedModel> Markets { get; set; }

        protected override int GenerateKey()
        {
            return Tournament.Key.GetHashCode() ^ HomeTeam.Key.GetHashCode() ^ AwayTeam.Key.GetHashCode();
        }
    }
}