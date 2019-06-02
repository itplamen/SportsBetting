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
        private readonly ICategoriesManager categoriesManager;
        private readonly ITournamentsManager tournamentsManager;

        public FeedManager(
            IOddsManager oddsManager,
            ITeamsManager teamsManager,
            IMarketsManager marketsManager,
            IMatchesManager matchesManager,
            ICategoriesManager categoriesManager,
            ITournamentsManager tournamentsManager)
        {
            this.oddsManager = oddsManager;
            this.teamsManager = teamsManager;
            this.marketsManager = marketsManager;
            this.matchesManager = matchesManager;
            this.categoriesManager = categoriesManager;
            this.tournamentsManager = tournamentsManager;
        }

        public void Manage(MatchFeedModel feedModel)
        {
            string categoryId = categoriesManager.Manage(feedModel.Tournament.Category);
            string tournamentId = tournamentsManager.Manage(feedModel.Tournament, categoryId);

            string homeTeamId = teamsManager.Manage(feedModel.HomeTeam);
            string awayTeamId = teamsManager.Manage(feedModel.AwayTeam);

            string matchId = matchesManager.Manage(feedModel, categoryId, tournamentId, homeTeamId, awayTeamId);

            foreach (var market in feedModel.Markets)
            {
                string marketId = marketsManager.Manage(market, matchId);
                oddsManager.Manage(market.Odds, marketId, matchId);
            }
        }
    }
}