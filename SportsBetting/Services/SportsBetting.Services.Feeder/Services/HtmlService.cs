namespace SportsBetting.Services.Feeder.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Services;

    public class HtmlService : IHtmlService
    {
        private const string BASE_URL = "https://gg.bet";

        public IList<string> GetOddNames(HtmlNode marketNode, Match match)
        {
            HtmlNodeCollection oddNodes = marketNode.SelectNodes(OddXPaths.NAME);

            if (oddNodes != null)
            {
                return oddNodes.Select(x => x.InnerText).ToList();
            }

            return new List<string>()
            {
                match.HomeTeam.Name,
                match.AwayTeam.Name
            };
        }

        public IEnumerable<string> GetMatchUrls(HtmlNode bettingContainer, string xpath)
        {
            HtmlNodeCollection hrefNodes = bettingContainer.SelectNodes(xpath);

            if (hrefNodes != null)
            {
                IEnumerable<string> urls = hrefNodes
                    .Select(x => x.GetAttributeValue("href", "noLink"))
                    .Select(x => $"{BASE_URL}{x}")
                    .Distinct();

                return urls;
            }

            return Enumerable.Empty<string>();
        }

        public HtmlNode GetContainer(string xpath, string pageSource)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageSource);

            HtmlNode container = document.DocumentNode.SelectSingleNode(xpath);

            return container;
        }

        public HtmlNode GetMarketContainer(HtmlNode marketNode)
        {
            HtmlNodeCollection containerNodes = marketNode.SelectNodes(ContainerXPaths.MARKET);

            if (containerNodes != null)
            {
                return containerNodes.FirstOrDefault();
            }

            return null;
        }

        public int GetTwoWayOddsCount(HtmlNode marketNode)
        {
            try
            {
                HtmlNode marketContainer = GetMarketContainer(marketNode);

                if (marketContainer != null && marketContainer.ChildNodes.Any())
                {
                    IEnumerable<HtmlNode> oddNodes = marketContainer.SelectNodes(OddXPaths.TWO_WAY_COUNT);

                    if (oddNodes != null)
                    {
                        return oddNodes.SelectMany(x => x.ChildNodes).Count();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int GetThreeWayOddsCount(HtmlNode marketNode)
        {
            try
            {
                HtmlNode marketContainer = GetMarketContainer(marketNode);

                if (marketContainer != null && marketContainer.ChildNodes.Any())
                {
                    HtmlNode oddsContainer = marketContainer.SelectSingleNode(ContainerXPaths.THREE_WAY_ODDS);

                    if (oddsContainer != null && oddsContainer.ChildNodes.Any())
                    {
                        return oddsContainer.ChildNodes.Count;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public OddResultStatus GetOddResultStatus(HtmlNode oddNode)
        {
            string oddClass = oddNode.GetAttributeValue("class", string.Empty);

            if (IsWin(oddClass))
            {
                return OddResultStatus.Win;
            }

            if (IsLoss(oddClass))
            {
                return OddResultStatus.Loss;
            }

            return OddResultStatus.NotResulted;
        }

        public bool HasHeader(HtmlNode marketNode)
        {
            HtmlNodeCollection headerNodes = marketNode.SelectNodes(OddXPaths.HEADER);

            return headerNodes != null && headerNodes.Any();
        }

        public bool IsSuspended(HtmlNode oddNode)
        {
            bool isDeactivated = oddNode.GetAttributeValue("title", string.Empty) == "Deactivated";
            bool isSuspended = OddXPaths.SUSPENDED.Any(x => oddNode.GetAttributeValue("class", string.Empty).Contains(x));

            return isDeactivated && isSuspended;
        }

        private bool IsWin(string oddClass)
        {
            return oddClass.Contains(OddXPaths.WIN_STATUS) ||
                oddClass.Contains(OddXPaths.HANDICAP_WIN_STATUS) ||
                oddClass.Contains(OddXPaths.CORRECT_SCORE_WIN_STATUS);
        }

        private bool IsLoss(string oddClass)
        {
            return oddClass.Contains(OddXPaths.LOSS_STATUS) ||
                oddClass.Contains(OddXPaths.HANDICAP_LOSS_STATUS) ||
                oddClass.Contains(OddXPaths.CORRECT_SCORE_LOSS_STATUS);
        }
    }
}