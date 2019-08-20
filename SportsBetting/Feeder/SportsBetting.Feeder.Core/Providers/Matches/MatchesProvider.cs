namespace SportsBetting.Feeder.Core.Providers.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using HtmlAgilityPack;

    using Newtonsoft.Json;

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
            HtmlNode matchInfo = matchContainer?.SelectSingleNode(MatchXPaths.HEADER_INFO_BOX);
            MatchFeedStatus status = GetStatus(matchInfo);
            DateTime startTime = GetStartTime(matchInfo);

            IEnumerable<TeamFeedModel> teams = teamsProvider.Get(matchContainer);
            TournamentFeedModel tournament = tournametsProvider.Get(matchInfo);

            MatchFeedModel match = ObjectFactory.CreateMatch(startTime, status, teams.First(), teams.Last(), tournament);
            match.Markets = marketsProvider.Get(matchContainer, match);

            return match;
        }

        private MatchFeedStatus GetStatus(HtmlNode matchInfo)
        {
            string status = matchInfo.ChildNodes[1].InnerText.Split(',').Last().Trim();
            string jsonStatus = JsonConvert.SerializeObject(status);
            MatchFeedStatus matchStatus = JsonConvert.DeserializeObject<MatchFeedStatus>(jsonStatus);

            return matchStatus;
        }

        private DateTime GetStartTime(HtmlNode matchInfo)
        {
            string startTime = matchInfo.LastChild.InnerText;

            startTime = startTime.Replace(", ", $" { DateTime.Now.Year } ");
            startTime = startTime.Replace(" +", ":00 +");

            DateTime gameTime = DateTime.ParseExact(startTime, "dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture);

            return gameTime;
        }
    }
}