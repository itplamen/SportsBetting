﻿namespace SportsBetting.Services.Feeder.Contracts.Providers
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IOddsProvider
    {
        IEnumerable<OddFeedModel> Get(HtmlNode marketNode, IList<string> oddNames, int marketKey);
    }
}