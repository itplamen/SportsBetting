namespace SportsBetting.Services.Feeder.Contracts.Providers
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface ITeamsProvider
    {
        IEnumerable<TeamFeedModel> Get(HtmlNode matchContainer);
    }
}