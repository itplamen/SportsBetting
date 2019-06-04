namespace SportsBetting.Services.Feeder.Contracts.Services
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IHtmlService
    {
        IList<string> GetOddNames(HtmlNode marketNode, MatchFeedModel match);

        IEnumerable<string> GetMatchUrls(HtmlNode bettingContainer, string xpath);

        HtmlNode GetContainer(string xpath, string pageSource);

        HtmlNode GetMarketContainer(HtmlNode marketNode);

        int GetTwoWayOddsCount(HtmlNode marketNode);

        int GetOddsCount(HtmlNode marketNode);

        OddResultFeedStatus GetOddResultStatus(HtmlNode oddNode);

        bool HasHeader(HtmlNode marketNode);

        bool IsSuspended(HtmlNode oddNode);
    }
}