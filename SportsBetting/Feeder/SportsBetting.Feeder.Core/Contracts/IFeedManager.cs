namespace SportsBetting.Feeder.Core.Contracts
{
    using HtmlAgilityPack;

    public interface IFeedManager
    {
        void Manage(HtmlNode matchContainer, string url, bool isLive);
    }
}