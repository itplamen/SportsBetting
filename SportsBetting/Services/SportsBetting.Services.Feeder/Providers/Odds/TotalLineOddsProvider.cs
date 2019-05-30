namespace SportsBetting.Services.Feeder.Providers.Odds
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Providers.Odds.Base;

    public class TotalLineOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 3;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public TotalLineOddsProvider(IHtmlService htmlService, IObjectFactory objectFactory, IOddsProvider oddsProvider)
            : base(htmlService, objectFactory)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketId)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                ICollection<OddFeedModel> odds = new List<OddFeedModel>();

                IEnumerable<KeyValuePair<string, List<HtmlNode>>> oddNodes = GetNodes(marketNode);
                oddNames = oddNames.Skip(1).ToList();

                foreach (var oddNode in oddNodes)
                {
                    for (int i = 0; i < oddNames.Count; i++)
                    {
                        OddFeedModel odd = BuildOdd(oddNode.Value[i], oddNames[i], i, marketId, oddNode.Key);
                        odds.Add(odd);
                    }
                }

                return odds;
            }

            return oddsProvider.Get(marketNode, oddNames, marketId);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetThreeWayOddsCount(marketNode);
            bool isOddsCountValid = oddNames.Count == ODDS_COUNT && oddsCount == ODDS_COUNT;

            return isOddsCountValid && htmlService.HasHeader(marketNode);
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode.FirstChild);
        }

        protected override decimal GetValue(HtmlNode oddNode)
        {
            decimal value = 0;
            decimal.TryParse(oddNode.LastChild.InnerText, out value);

            return value;
        }

        private IEnumerable<KeyValuePair<string, List<HtmlNode>>> GetNodes(HtmlNode marketNode)
        {
            HtmlNodeCollection containers = marketNode.SelectNodes(ContainerXPaths.MARKET);

            return containers
                .GroupBy(x => x.FirstChild.InnerText)
                .ToDictionary(x => x.Key, y => y.SelectMany(node => node.SelectNodes(OddXPaths.NODE))
                .ToList());
        }
    }
}