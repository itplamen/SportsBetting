namespace SportsBetting.Services.Feeder.Factories
{
    using System;

    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;

    public class ObjectFactory : IObjectFactory
    {
        public MarketFeedModel CreateMarket(string name, int matchId)
        {
            return new MarketFeedModel()
            {
                Name = name,
                MatchId = matchId
            };
        }

        public MatchFeedModel CreateMatch(
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

        public OddFeedModel CreateOdd(
            string name, 
            decimal value, 
            bool isSuspended, 
            OddResultFeedStatus resultStatus, 
            int rank, 
            int matketId, 
            string header = null)
        {
            return new OddFeedModel()
            {
                Name = name,
                Value = value,
                Header = header,
                IsSuspended = isSuspended,
                ResultStatus = resultStatus,
                Rank = rank,
                MarketId = matketId
            };
        }

        public TeamFeedModel CreateTeam(string name, int? score)
        {
            return new TeamFeedModel()
            {
                Name = name,
                Score = score
            };
        }

        public TournamentFeedModel CreateTournament(string name, string category)
        {
            return new TournamentFeedModel()
            {
                Name = name,
                Category = category
            };
        }
    }
}