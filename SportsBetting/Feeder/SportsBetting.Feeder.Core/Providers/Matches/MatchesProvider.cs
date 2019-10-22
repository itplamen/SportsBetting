namespace SportsBetting.Feeder.Core.Providers.Matches
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class MatchesProvider : IMatchesProvider
    {
        private readonly ITeamsProvider teamsProvider;
        private readonly IMarketsProvider marketsProvider;
        private readonly ITournametsProvider tournametsProvider;

        public MatchesProvider(ITeamsProvider teamsProvider, IMarketsProvider marketsProvider, ITournametsProvider tournametsProvider)
        {
            this.teamsProvider = teamsProvider;
            this.marketsProvider = marketsProvider;
            this.tournametsProvider = tournametsProvider;
        }

        public MatchFeedModel Get(HtmlNode matchContainer)
        {
            HtmlNode matchInfo = matchContainer.SelectSingleNode(MatchXPaths.HEADER_INFO_BOX);

            MatchFeedType type = GetType(matchInfo);
            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);
            TournamentFeedModel tournament = tournametsProvider.Get(matchInfo);

            MatchFeedModel match = ObjectFactory.CreateMatch(type, teams.First(), teams.Last(), tournament);
            match.Markets = marketsProvider.Get(matchContainer, match);

            return match;
        }

        private MatchFeedType GetType(HtmlNode matchInfo)
        {
            HtmlNode liveScoreNode = matchInfo.SelectSingleNode(MatchXPaths.LIVE_SCORE);

            if (liveScoreNode != null)
            {
                return MatchFeedType.Live;
            }

            return MatchFeedType.Prematch;
        }
    }
}