namespace SportsBetting.Services.Feeder.Providers.Markets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Contracts.Services;

    public class MarketsProvider : IMarketsProvider
    {
        private readonly IHtmlService htmlService;
        private readonly IOddsProvider oddsProvider;
        private readonly IObjectFactory objectFactory;

        public MarketsProvider(IHtmlService htmlService, IOddsProvider oddsProvider, IObjectFactory objectFactory)
        {
            this.htmlService = htmlService;
            this.oddsProvider = oddsProvider;
            this.objectFactory = objectFactory;
        }

        public IEnumerable<MarketFeedModel> Get(HtmlNode matchContainer, MatchFeedModel match)
        {
            ICollection<MarketFeedModel> markets = new List<MarketFeedModel>();

            HtmlNodeCollection marketNodes = matchContainer.SelectNodes(MatchXPaths.MARKETS);

            foreach (var marketNode in marketNodes)
            {
                MarketFeedModel market = objectFactory.CreateMarket(marketNode.FirstChild.FirstChild.InnerText, match.Key);
                market.Odds = GetOdds(marketNode, match, market.Key);

                markets.Add(market);
            }

            return markets;
        }

        private IEnumerable<OddFeedModel> GetOdds(HtmlNode marketNode, MatchFeedModel match, int marketKey)
        {
            try
            {
                IList<string> oddNames = htmlService.GetOddNames(marketNode, match);
                IEnumerable<OddFeedModel> odds = oddsProvider.Get(marketNode, oddNames, marketKey);

                return odds;
            }
            catch (Exception ex)
            {
            }

            return Enumerable.Empty<OddFeedModel>();
        }
    }
}