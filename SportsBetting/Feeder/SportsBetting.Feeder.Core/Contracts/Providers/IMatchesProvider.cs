namespace SportsBetting.Feeder.Core.Contracts.Providers
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IMatchesProvider
    {
        MatchFeedModel Get(HtmlNode matchContainer);
    }
}