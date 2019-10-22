namespace SportsBetting.Feeder.Core.Managers
{
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;

    public class FeedManager : IFeedManager
    {
        private readonly IOddsManager oddsManager;
        private readonly ITeamsManager teamsManager;
        private readonly IMarketsManager marketsManager;
        private readonly IMatchesManager matchesManager;
        private readonly ITournamentsManager tournamentsManager;

        public FeedManager(
            IOddsManager oddsManager,
            ITeamsManager teamsManager,
            IMarketsManager marketsManager,
            IMatchesManager matchesManager,
            ITournamentsManager tournamentsManager)
        {
            this.oddsManager = oddsManager;
            this.teamsManager = teamsManager;
            this.marketsManager = marketsManager;
            this.matchesManager = matchesManager;
            this.tournamentsManager = tournamentsManager;
        }

        public void Manage(MatchFeedModel feedModel)
        {
            string tournamentId = tournamentsManager.Manage(feedModel.Tournament);

            string homeTeamId = teamsManager.Manage(feedModel.HomeTeam);
            string awayTeamId = teamsManager.Manage(feedModel.AwayTeam);

            string matchId = matchesManager.Manage(feedModel, tournamentId, homeTeamId, awayTeamId);

            foreach (var market in feedModel.Markets)
            {
                string marketId = marketsManager.Manage(market, matchId);
                oddsManager.Manage(market.Odds, marketId, matchId);
            }
        }
    }
}