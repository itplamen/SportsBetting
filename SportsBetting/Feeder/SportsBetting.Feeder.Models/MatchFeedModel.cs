namespace SportsBetting.Feeder.Models
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models.Base;

    public class MatchFeedModel : BaseFeedModel
    {
        public MatchFeedModel()
        {
            Markets = new List<MarketFeedModel>();
        }

        public TeamFeedModel HomeTeam { get; set; }

        public TeamFeedModel AwayTeam { get; set; }

        public DateTime StartTime { get; set; }

        public MatchFeedStatus Status { get; set; }

        public TournamentFeedModel Tournament { get; set; }

        public IEnumerable<MarketFeedModel> Markets { get; set; }

        protected override int GenerateId()
        {
            int id = Tournament.Id.GetHashCode() ^ HomeTeam.Id.GetHashCode() ^ AwayTeam.Id.GetHashCode();

            return Math.Abs(id);
        }
    }
}