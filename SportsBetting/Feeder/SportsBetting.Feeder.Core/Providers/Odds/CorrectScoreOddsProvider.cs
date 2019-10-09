namespace SportsBetting.Feeder.Core.Providers.Odds
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Providers.Odds.Base;
    using SportsBetting.Feeder.Models;

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

        public IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames)
        {
            if (ShouldGet(marketNode, oddNames))
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
                            OddFeedModel odd = BuildOdd(oddNode, header, rank, header);

                            odds.Add(odd);

                            rank += 2;
                        }
                    }
                }

                return odds;
            }

            return oddsProvider.Get(marketNode, oddNames);
        }

        protected override bool ShouldGet(HtmlNode marketNode, IList<string> oddNames)
        {
            return oddNames.Count == ODDS_COUNT && marketNode.FirstChild.FirstChild.InnerText.ToLower().Contains("score");
        }

        protected override bool IsSuspended(HtmlNode oddNode)
        {
            return htmlService.IsSuspended(oddNode.LastChild.LastChild);
        }
    }
}