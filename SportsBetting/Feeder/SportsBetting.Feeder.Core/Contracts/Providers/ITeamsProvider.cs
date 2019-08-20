namespace SportsBetting.Feeder.Core.Contracts.Providers
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface ITeamsProvider
    {
        IEnumerable<TeamFeedModel> Get(HtmlNode matchContainer);
    }
}