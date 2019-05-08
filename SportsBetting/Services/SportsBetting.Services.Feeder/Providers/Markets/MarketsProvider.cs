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

        public IEnumerable<Market> Get(HtmlNode matchContainer, Match match)
        {
            ICollection<Market> markets = new List<Market>();

            HtmlNodeCollection marketNodes = matchContainer.SelectNodes(MatchXPaths.MARKETS);

            foreach (var marketNode in marketNodes)
            {
                Market market = objectFactory.CreateMarket(marketNode.FirstChild.FirstChild.InnerText, match.Id);
                market.Odds = GetOdds(marketNode, match, market.Id);

                markets.Add(market);
            }

            return markets;
        }

        private IEnumerable<Odd> GetOdds(HtmlNode marketNode, Match match, int marketId)
        {
            try
            {
                IList<string> oddNames = htmlService.GetOddNames(marketNode, match);
                IEnumerable<Odd> odds = oddsProvider.Get(marketNode, oddNames, marketId);

                return odds;
            }
            catch (Exception ex)
            {
            }

            return Enumerable.Empty<Odd>();
        }
    }
}