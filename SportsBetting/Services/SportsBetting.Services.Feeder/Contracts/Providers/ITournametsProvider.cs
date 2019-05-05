namespace SportsBetting.Services.Feeder.Contracts.Providers
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Models;

    public interface ITournametsProvider
    {
        Tournament Get(HtmlNode matchInfo);
    }
}