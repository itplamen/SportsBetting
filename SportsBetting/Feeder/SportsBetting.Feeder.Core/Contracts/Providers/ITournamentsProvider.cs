namespace SportsBetting.Feeder.Core.Contracts.Providers
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface ITournamentsProvider
    {
        TournamentFeedModel Get(HtmlNode matchInfo);
    }
}