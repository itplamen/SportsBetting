namespace SportsBetting.Services.Feeder.Providers.Odds
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers.Odds;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Providers.Odds.Base;

    public class TwoWayOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 2;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public TwoWayOddsProvider(IHtmlService htmlService, IObjectFactory objectFactory, IOddsProvider oddsProvider)
            : base(htmlService, objectFactory)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<Odd> Get(HtmlNode marketNode, IList<string> oddNames, int marketId)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                ICollection<Odd> odds = new List<Odd>();
                IList<HtmlNode> oddNodes = marketNode.SelectNodes(OddXPaths.NODE);

                for (int i = 0; i < oddNames.Count; i++)
                {
                    Odd odd = BuildOdd(oddNodes[i], oddNames[i], i, marketId);
                    odds.Add(odd);
                }

                return odds;
            }

            return oddsProvider.Get(marketNode, oddNames, marketId);
        }

        protected override decimal GetValue(HtmlNode oddNode)
        {
            decimal value = 0;
            decimal.TryParse(oddNode.LastChild.InnerText, out value);

            return value;
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetTwoWayOddsCount(marketNode);
            bool isCountValid = oddNames.Count == ODDS_COUNT && oddsCount == ODDS_COUNT;

            return isCountValid && !htmlService.HasHeader(marketNode);
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode.LastChild);
        }
    }
}