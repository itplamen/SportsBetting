namespace SportsBetting.Feeder.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Feeder.Models.Base;

    public class MatchFeedModel : BaseFeedModel
    {
        public MatchFeedModel()
        {
            Markets = new List<MarketFeedModel>();
        }

        public string Url { get; set; }

        public bool IsLive { get; set; }

        public TeamFeedModel HomeTeam { get; set; }

        public TeamFeedModel AwayTeam { get; set; }

        public DateTime StartTime { get; set; }

        public MatchFeedStatus Status { get; set; }

        public TournamentFeedModel Tournament { get; set; }

        public IEnumerable<MarketFeedModel> Markets { get; set; }

        protected override int GenerateId()
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                string[] splittedUrl = Url.Split(':');

                if (splittedUrl.Any())
                {
                    return int.Parse(splittedUrl.Last());
                }
            }

            return 0;
        }
    }
}