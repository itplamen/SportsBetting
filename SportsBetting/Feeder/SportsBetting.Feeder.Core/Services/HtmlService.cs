namespace SportsBetting.Feeder.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using SportsBetting.Common.Constants;
    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Models;

    public class HtmlService : IHtmlService
    {
        public IList<string> GetOddNames(HtmlNode marketNode)
        {
            HtmlNodeCollection oddNodes = marketNode.SelectNodes(OddXPaths.NAMES);

            return oddNodes.Select(x => x.InnerText).ToList();
        }

        public IEnumerable<string> GetMatchUrls(string xpath, string pageSource)
        {
            HtmlDocument document = LoadHtml(pageSource);
            IEnumerable<HtmlNode> htmlNodes = document.DocumentNode.SelectNodes(xpath);
            IEnumerable<string> urls = GetUrls(pageSource, MatchXPaths.EVENT_URL, htmlNodes);

            return urls;            
        }

        public HtmlNode GetMatchContainer(string xpath, string pageSource)
        {
            HtmlDocument document = LoadHtml(pageSource);
            HtmlNode container = document.DocumentNode.SelectSingleNode(xpath);

            return container;
        }

        public int GetTwoWayOddsCount(HtmlNode marketNode)
        {
            HtmlNode marketContainer = GetMarketContainer(marketNode);

            if (marketContainer != null && marketContainer.ChildNodes.Any())
            {

                IEnumerable<HtmlNode> oddNodes = marketContainer.SelectNodes(OddXPaths.NODE);

                if (oddNodes != null)
                {
                    return oddNodes.Count();
                }
            }

            return 0;
        }

        public int GetOddsCount(HtmlNode marketNode)
        {
            HtmlNode marketContainer = GetMarketContainer(marketNode);

            if (marketContainer != null && marketContainer.ChildNodes.Any())
            {
                return marketContainer.ChildNodes.Count;
            }

            return 0;
        }

        public OddResultFeedStatus GetOddResultStatus(HtmlNode oddNode)
        {
            HtmlNode resultedNode = oddNode.SelectSingleNode(OddXPaths.RESULTED_STATUS);

            if (resultedNode != null)
            {
                if (resultedNode.InnerText.ToLower() == "loss")
                {
                    return OddResultFeedStatus.Loss;
                }

                if (resultedNode.InnerText.ToLower() == "win")
                {
                    return OddResultFeedStatus.Win;
                }
            }

            return OddResultFeedStatus.NotResulted;
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

        private HtmlNode GetMarketContainer(HtmlNode marketNode)
        {
            HtmlNodeCollection containerNodes = marketNode.SelectNodes(ContainerXPaths.MARKET);

            if (containerNodes != null)
            {
                return containerNodes.FirstOrDefault();
            }

            return null;
        }

        private IEnumerable<string> GetUrls(string pageSource, string xpath, IEnumerable<HtmlNode> htmlNodes)
        {
            if (htmlNodes != null && htmlNodes.SelectMany(x => x.ChildNodes).Any())
            {
                IEnumerable<HtmlNode> nodes = htmlNodes.SelectMany(x => x.SelectNodes(xpath));

                if (nodes != null)
                {
                    return SelectUrls(nodes);
                }
            }

            return Enumerable.Empty<string>();
        }

        private IEnumerable<string> SelectUrls(IEnumerable<HtmlNode> htmlNodes)
        {
            IEnumerable<string> urls = htmlNodes
                .Select(t => t.GetAttributeValue("href", "noLink"))
                .Select(y => $"{CommonConstants.BASE_URL}{y}")
                .Distinct();

            return urls;
        }

        private HtmlDocument LoadHtml(string pageSource)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageSource);

            return document;
        }
    }
}