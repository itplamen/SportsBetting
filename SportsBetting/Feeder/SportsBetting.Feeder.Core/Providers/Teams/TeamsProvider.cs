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

            if (!HasTeamNames(names))
            {
                return null;
            }

            IEnumerable<HtmlNode> scoreNodes = matchContainer.SelectNodes(TeamXPaths.SCORE);

            TeamFeedModel homeTeam = BuildTeam(names.First(), scoreNodes?.First(), matchContainer);
            TeamFeedModel awayTeam = BuildTeam(names.Last(), scoreNodes?.Last(), matchContainer);

            return new List<TeamFeedModel>() { homeTeam, awayTeam };
        }

        private IEnumerable<string> ParseNames(HtmlNode matchContainer)
        {
            IEnumerable<string> teamNames = matchContainer
                ?.SelectNodes(TeamXPaths.NAME)
                ?.Select(x => x.InnerText.Trim())
                ?.ToList();

            return teamNames;
        }

        private TeamFeedModel BuildTeam(string name, HtmlNode scoreNode, HtmlNode matchContainer)
        {
            int score;
            int.TryParse(scoreNode?.InnerText, out score);

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

        private bool HasTeamNames(IEnumerable<string> names)
        {
            return names != null && names.Count(name => !string.IsNullOrEmpty(name)) >= 2;
        }
    }
}