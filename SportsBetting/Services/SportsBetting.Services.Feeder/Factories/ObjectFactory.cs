namespace SportsBetting.Services.Feeder.Factories
{
    using System;
    using System.Collections.Generic;

    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;

    public class ObjectFactory : IObjectFactory
    {
        public Market CreateMarket(string name, int matchId, IEnumerable<Odd> odds)
        {
            return new Market()
            {
                Name = name,
                MatchId = matchId,
                Odds = odds
            };
        }

        public Match CreateMatch(
            string url,
            bool isLive,
            DateTime startTime,
            MatchStatus status,
            Team homeTeam,
            Team awayTeam,
            Tournament tournament,
            IEnumerable<Market> markets)
        {
            return new Match()
            {
                Url = url,
                IsLive = isLive,
                StartTime = startTime,
                Status = status,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Tournament = tournament,
                Markets = markets
            };
        }

        public Odd CreateOdd(
            string name, 
            decimal value, 
            bool isSuspended, 
            OddResultStatus resultStatus, 
            int rank, 
            int matketId, 
            string header = null)
        {
            return new Odd()
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

        public Team CreateTeam(string name, int? score)
        {
            return new Team()
            {
                Name = name,
                Score = score
            };
        }

        public Tournament CreateTournament(string name, string category)
        {
            return new Tournament()
            {
                Name = name,
                Category = category
            };
        }
    }
}