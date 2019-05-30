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

    public class CorrectScoreOddsProvider : BaseOddsProvider, IOddsProvider
    {
        private const int ODDS_COUNT = 2;

        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;

        public CorrectScoreOddsProvider(IHtmlService htmlService, IObjectFactory objectFactory, IOddsProvider oddsProvider)
            : base(htmlService, objectFactory)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
        }

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketId)
        {
            if (ShouldGet(marketNode, oddNames))
            {
                HtmlNodeCollection oddNodes = marketNode.SelectNodes(OddXPaths.TWO_WAY_COUNT);

                if (oddNodes != null)
                {
                    return GetTwoColumnOdds(oddNodes, marketId);
                }

                return GetFourColumnOdds(marketNode, marketId);
            }

            return oddsProvider.Get(marketNode, oddNames, marketId);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            return oddNames.Count == ODDS_COUNT && marketNode.FirstChild.FirstChild.InnerText.ToLower().Contains("score");
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode.LastChild.LastChild);
        }

        private IEnumerable<OddFeedModel> GetTwoColumnOdds(HtmlNodeCollection oddNodes, int marketId)
        {
            ICollection<OddFeedModel> odds = new List<OddFeedModel>();

            for (int i = 0; i < oddNodes.Count; i++)
            {
                string header = oddNodes[i].FirstChild.InnerText;
                OddFeedModel odd = BuildOdd(oddNodes[i], header, i, marketId, header);

                odds.Add(odd);
            }

            return odds;
        }

        private IEnumerable<OddFeedModel> GetFourColumnOdds(HtmlNode marketNode, int marketId)
        {
            ICollection<OddFeedModel> odds = new List<OddFeedModel>();
            List<string> columnXPaths = OddXPaths.CORRECT_SCORE_COLUMNS.ToList();

            for (int i = 0; i < columnXPaths.Count; i++)
            {
                int rank = i;
                IList<HtmlNode> oddNodes = GetNodes(marketNode, columnXPaths[i]);

                if (oddNodes != null)
                {
                    foreach (var oddNode in oddNodes)
                    {
                        string header = oddNode.FirstChild.InnerText;
                        OddFeedModel odd = BuildOdd(oddNode, header, rank, marketId, header);

                        odds.Add(odd);

                        rank += 2;
                    }
                }
            }

            return odds;
        }

        private IList<HtmlNode> GetNodes(HtmlNode marketNode, string xpath)
        {
            HtmlNodeCollection oddValuesNodes = marketNode.SelectNodes(xpath);

            if (oddValuesNodes != null)
            {
                return oddValuesNodes.First().SelectNodes(OddXPaths.CORRECT_SCORE_VALUE);
            }

            return marketNode.SelectNodes(OddXPaths.TWO_WAY_COUNT);
        }
    }
}