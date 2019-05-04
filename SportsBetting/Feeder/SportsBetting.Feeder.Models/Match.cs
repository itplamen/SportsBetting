namespace SportsBetting.Feeder.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Feeder.Models.Base;

    public class Match : BaseModel
    {
        public Match()
        {
            Markets = new List<Market>();
        }

        public string Url { get; set; }

        public bool IsLive { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public DateTime StartTime { get; set; }

        public MatchStatus Status { get; set; }

        public Tournament Tournament { get; set; }

        public IEnumerable<Market> Markets { get; set; }

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