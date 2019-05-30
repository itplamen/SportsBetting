namespace SportsBetting.Feeder.Core
{
    using HtmlAgilityPack;

    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Providers;

    public class FeedManager : IFeedManager
    {
        private readonly IMatchesProvider matchesProvider;

        public FeedManager(IMatchesProvider matchesProvider)
        {
            this.matchesProvider = matchesProvider;
        }

        public void Manage(HtmlNode matchContainer, string url, bool isLive)
        {
            Match match = matchesProvider.Get(matchContainer, url, isLive);
        }
    }
}