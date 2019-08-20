namespace SportsBetting.Feeder.Core.Contracts.Providers
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface ITournametsProvider
    {
        TournamentFeedModel Get(HtmlNode matchInfo);
    }
}