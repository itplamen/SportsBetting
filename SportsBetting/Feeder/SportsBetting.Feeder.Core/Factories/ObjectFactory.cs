namespace SportsBetting.Feeder.Core.Factories
{
    using System;

    using SportsBetting.Feeder.Models;

    public static class ObjectFactory
    {
        public static MarketFeedModel CreateMarket(string name, int matchKey)
        {
            return new MarketFeedModel()
            {
                Name = name,
                MatchKey = matchKey
            };
        }

        public static MatchFeedModel CreateMatch(
            DateTime startTime,
            MatchFeedStatus status,
            TeamFeedModel homeTeam,
            TeamFeedModel awayTeam,
            TournamentFeedModel tournament)
        {
            return new MatchFeedModel()
            {
                StartTime = startTime,
                Status = status,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Tournament = tournament
            };
        }

        public static OddFeedModel CreateOdd(
            string name, 
            decimal value, 
            bool isSuspended, 
            OddResultFeedStatus resultStatus,
            int rank,
            decimal header = 0,
            string symbol = null)
        {
            return new OddFeedModel()
            {
                Name = name,
                Value = value,
                Header = header,
                Symbol = symbol,
                IsSuspended = isSuspended,
                ResultStatus = resultStatus,
                Rank = rank
            };
        }

        public static TeamFeedModel CreateTeam(string name, int? score)
        {
            return new TeamFeedModel()
            {
                Name = name,
                Score = score
            };
        }

        public static TournamentFeedModel CreateTournament(string name, string category)
        {
            return new TournamentFeedModel()
            {
                Name = name,
                Category = category
            };
        }
    }
}