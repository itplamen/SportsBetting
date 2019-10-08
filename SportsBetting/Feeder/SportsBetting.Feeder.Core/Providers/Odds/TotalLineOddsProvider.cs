namespace SportsBetting.Feeder.Core.Providers.Odds
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Providers.Odds.Base;
    using SportsBetting.Feeder.Models;

    public class TotalLineOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 3;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public TotalLineOddsProvider(IHtmlService htmlService, IOddsProvider oddsProvider)
            : base(htmlService)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames)
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
                        OddFeedModel odd = BuildOdd(oddNode.Value[i], oddNames[i], i, OddFeedType.TotalLine, oddNode.Key);
                        odds.Add(odd);
                    }
                }

                return odds;
            }

            return oddsProvider.Get(marketNode, oddNames);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetOddsCount(marketNode);
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