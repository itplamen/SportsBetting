namespace SportsBetting.Services.Feeder.Contracts.Providers
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface IMatchesProvider
    {
        MatchFeedModel Get(HtmlNode matchContainer, string url, bool isLive);
    }
}