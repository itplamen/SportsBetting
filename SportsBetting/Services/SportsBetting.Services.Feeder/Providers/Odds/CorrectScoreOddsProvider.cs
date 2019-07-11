namespace SportsBetting.Services.Feeder.Providers.Odds
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Providers.Odds.Base;

    public class CorrectScoreOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 2;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public CorrectScoreOddsProvider(IHtmlService htmlService, IOddsProvider oddsProvider)
            : base(htmlService)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketKey)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                HtmlNodeCollection oddNodes = marketNode.SelectNodes(OddXPaths.NODE);

                if (oddNodes != null)
                {
                    return GetTwoColumnOdds(oddNodes, marketKey);
                }

                return GetFourColumnOdds(marketNode, marketKey);
            }

            return oddsProvider.Get(marketNode, oddNames, marketKey);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            return oddNames.Count == ODDS_COUNT && marketNode.FirstChild.FirstChild.InnerText.ToLower().Contains("score");
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode.LastChild.LastChild);
        }

        private IEnumerable<OddFeedModel> GetTwoColumnOdds(HtmlNodeCollection oddNodes, int marketKey)
        {
            ICollection<OddFeedModel> odds = new List<OddFeedModel>();

            for (int i = 0; i < oddNodes.Count; i++)
            {
                string header = oddNodes[i].FirstChild.InnerText;
                OddFeedModel odd = BuildOdd(oddNodes[i], header, i, marketKey, header);

                odds.Add(odd);
            }

            return odds;
        }

        private IEnumerable<OddFeedModel> GetFourColumnOdds(HtmlNode marketNode, int marketKey)
        {
            ICollection<OddFeedModel> odds = new List<OddFeedModel>();
            HtmlNodeCollection oddNodesCollection = marketNode.SelectNodes(OddXPaths.CORRECT_SCORE_NODE);

            for (int i = 0; i < oddNodesCollection.Count; i++)
            {
                int rank = i;

                if (oddNodesCollection[i].ChildNodes != null)
                {
                    foreach (var oddNode in oddNodesCollection[i].ChildNodes)
                    {
                        string header = oddNode.FirstChild.InnerText;
                        OddFeedModel odd = BuildOdd(oddNode, header, rank, marketKey, header);

                        odds.Add(odd);

                        rank += 2;
                    }
                }
            }

            return odds;
        }
    }
}