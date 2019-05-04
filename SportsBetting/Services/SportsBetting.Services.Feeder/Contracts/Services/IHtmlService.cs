namespace SportsBetting.Services.Feeder.Contracts.Services
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IHtmlService
    {
        IList<string> GetOddNames(HtmlNode marketNode, Match match);

        HtmlNode GetMarketContainer(HtmlNode marketNode);

        int GetTwoWayOddsCount(HtmlNode marketNode);

        int GetThreeWayOddsCount(HtmlNode marketNode);

        OddResultStatus GetOddResultStatus(HtmlNode oddNode);

        bool HasHeader(HtmlNode marketNode);

        bool IsSuspended(HtmlNode oddNode);
    }
}