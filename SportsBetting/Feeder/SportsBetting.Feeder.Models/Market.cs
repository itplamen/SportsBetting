namespace SportsBetting.Feeder.Models
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models.Base;

    public class Market : BaseModel
    {
        public Market()
        {
            Odds = new List<Odd>();
        }

        public string Name { get; set; }

        public int MatchId { get; set; }

        public IEnumerable<Odd> Odds { get; set; }

        protected override int GenerateId()
        {
            return Math.Abs(Name.GetHashCode() ^ MatchId.GetHashCode());
        }
    }
}