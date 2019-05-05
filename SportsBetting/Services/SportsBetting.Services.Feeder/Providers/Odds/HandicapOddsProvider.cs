namespace SportsBetting.Services.Feeder.Providers.Odds
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers.Odds;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Providers.Odds.Base;

    public class HandicapOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 2;

        private readonly IHtmlService htmlService;

        public HandicapOddsProvider(IHtmlService htmlService, IObjectFactory objectFactory)
            : base(htmlService, objectFactory)
        {
            this.htmlService = htmlService;
        }

        public IEnumerable<Odd> Get(HtmlNode marketNode, IList<string> oddNames, int marketId)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                ICollection<Odd> odds = new List<Odd>();
                IList<HtmlNode> oddNodes = marketNode.SelectNodes(ContainerXPaths.HANDICAP_ODDS);

                for (int i = 0; i < oddNodes.Count; i++)
                {
                    int nameIndex = i % 2 == 0 ? 0 : 1;
                    string header = oddNodes[i].FirstChild.InnerText;

                    Odd odd = BuildOdd(oddNodes[i], oddNames[nameIndex], i, marketId, header);
                    odds.Add(odd);
                }

                return odds;
            }

            return Enumerable.Empty<Odd>();
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetTwoWayOddsCount(marketNode);
            bool isOddsCountValid = oddNames.Count == ODDS_COUNT && oddsCount == ODDS_COUNT;

            return isOddsCountValid && marketNode.FirstChild.FirstChild.InnerText.ToLower().Contains("handicap");
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            HtmlNode selectedOdd = oddNode.SelectSingleNode(OddXPaths.HANDICAP_ROW);

            if (selectedOdd != null)
            {
                return htmlService.IsSuspended(selectedOdd.FirstChild);
            }

            return false;
        }
    }
}