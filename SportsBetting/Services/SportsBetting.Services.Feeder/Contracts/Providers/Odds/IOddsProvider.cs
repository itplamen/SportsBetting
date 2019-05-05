namespace SportsBetting.Services.Feeder.Contracts.Providers.Odds
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IOddsProvider
    {
        IEnumerable<Odd> Get(HtmlNode marketNode, IList<string> oddNames, int marketId);
    }
}