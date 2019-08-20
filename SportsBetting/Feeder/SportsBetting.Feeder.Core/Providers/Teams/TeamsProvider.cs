namespace SportsBetting.Feeder.Core.Providers.Teams
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Models;

    public class TeamsProvider : ITeamsProvider
    {
        public IEnumerable<TeamFeedModel> Get(HtmlNode matchContainer)
        {
            IEnumerable<string> names = ParseNames(matchContainer);

            if (!names.Any())
            {
                return null;
            }

            TeamFeedModel homeTeam = BuildTeam(names.First(), TeamXPaths.HOME_TEAM_SCORE, matchContainer);
            TeamFeedModel awayTeam = BuildTeam(names.Last(), TeamXPaths.AWAY_TEAM_SCORE, matchContainer);

            return new List<TeamFeedModel>() { homeTeam, awayTeam };
        }

        private IEnumerable<string> ParseNames(HtmlNode matchContainer)
        {
            IEnumerable<string> teamNames = matchContainer
                .SelectNodes(TeamXPaths.NAME)
                .Select(x => x.InnerText.Trim())
                .ToList();

            return teamNames;
        }

        private TeamFeedModel BuildTeam(string name, string scoreXPath, HtmlNode matchContainer)
        {
            int? score = GetScore(matchContainer, scoreXPath);
            TeamFeedModel team = ObjectFactory.CreateTeam(name, score);

            return team;
        }

        private int? GetScore(HtmlNode matchContainer, string xpath)
        {
            string score = matchContainer.SelectSingleNode(xpath)?.InnerText;
            int parsedScore = 0;

            if (int.TryParse(score, out parsedScore))
            {
                return parsedScore;
            }

            return null;
        }
    }
}