namespace SportsBetting.Services.Feeder.Providers.Teams
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;

    public class TeamsProvider : ITeamsProvider
    {
        private readonly IObjectFactory objectFactory;

        public TeamsProvider(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public IEnumerable<Team> Get(HtmlNode matchContainer)
        {
            IEnumerable<string> names = ParseNames(matchContainer);

            if (!names.Any())
            {
                return null;
            }

            Team homeTeam = BuildTeam(names.First(), TeamXPaths.HOME_TEAM_SCORE, matchContainer);
            Team awayTeam = BuildTeam(names.Last(), TeamXPaths.AWAY_TEAM_SCORE, matchContainer);

            return new List<Team>() { homeTeam, awayTeam };
        }

        private IEnumerable<string> ParseNames(HtmlNode matchContainer)
        {
            IEnumerable<string> teamNames = matchContainer
                .SelectNodes(TeamXPaths.NAME)
                .Select(x => x.InnerText.Trim())
                .ToList();

            return teamNames;
        }

        private Team BuildTeam(string name, string scoreXPath, HtmlNode matchContainer)
        {
            int? score = GetScore(matchContainer, scoreXPath);
            Team team = objectFactory.CreateTeam(name, score);

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