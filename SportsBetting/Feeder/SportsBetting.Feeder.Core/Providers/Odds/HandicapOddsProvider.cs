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

    public class HandicapOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 2;

        private readonly IHtmlService htmlService;

        public HandicapOddsProvider(IHtmlService htmlService)
            : base(htmlService)
        {
            this.htmlService = htmlService;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketKey)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                ICollection<OddFeedModel> odds = new List<OddFeedModel>();
                IList<HtmlNode> oddNodes = marketNode.SelectNodes(ContainerXPaths.HANDICAP_ODDS);

                for (int i = 0; i < oddNodes.Count; i++)
                {
                    int nameIndex = i % 2 == 0 ? 0 : 1;
                    string header = oddNodes[i].FirstChild.InnerText;

                    OddFeedModel odd = BuildOdd(oddNodes[i], oddNames[nameIndex], i, marketKey, header);
                    odds.Add(odd);
                }

                return odds;
            }

            return Enumerable.Empty<OddFeedModel>();
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            int oddsCount = htmlService.GetOddsCount(marketNode);
            bool isOddsCountValid = oddNames.Count == ODDS_COUNT && oddsCount == ODDS_COUNT;

            return isOddsCountValid && marketNode.FirstChild.FirstChild.InnerText.ToLower().Contains("handicap");
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            HtmlNode selectedOdd = oddNode.SelectSingleNode(OddXPaths.HANDICAP_NODE);

            if (selectedOdd != null)
            {
                return htmlService.IsSuspended(selectedOdd.FirstChild);
            }

            return false;
        }
    }
}