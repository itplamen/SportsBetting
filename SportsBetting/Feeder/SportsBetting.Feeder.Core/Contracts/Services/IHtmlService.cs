﻿namespace SportsBetting.Feeder.Core.Contracts.Services
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IHtmlService
    {
        IList<string> GetOddNames(HtmlNode marketNode);

        IEnumerable<string> GetMatchUrls(string xpath, string pageSource);

        HtmlNode GetMatchContainer(string xpath, string pageSource);

        int GetTwoWayOddsCount(HtmlNode marketNode);

        int GetOddsCount(HtmlNode marketNode);

        OddResultFeedStatus GetOddResultStatus(HtmlNode oddNode);

        bool HasHeader(HtmlNode marketNode);

        bool IsSuspended(HtmlNode oddNode);
    }
}