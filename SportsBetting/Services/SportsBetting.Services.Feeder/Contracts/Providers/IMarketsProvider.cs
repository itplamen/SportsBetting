namespace SportsBetting.Services.Feeder.Contracts.Providers
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IMarketsProvider
    {
        IEnumerable<Market> Get(HtmlNode matchContainer, Match match);
    }
}