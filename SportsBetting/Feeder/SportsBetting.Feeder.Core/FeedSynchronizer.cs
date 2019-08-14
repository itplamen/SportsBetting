namespace SportsBetting.Feeder.Core
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using OpenQA.Selenium.Remote;

    using SportsBetting.Common.Constants;
    using SportsBetting.Common.XPaths;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Contracts.Services;

    public class FeedSynchronizer : ISynchronizer
    {
        private readonly IFeedManager feedManager;
        private readonly IHtmlService htmlService;
        private readonly RemoteWebDriver webDriver;
        private readonly IWebPagesService webPagesService;
        private readonly IMatchesProvider matchesProvider;
        private readonly IUnprocessedFeedManager unprocessedFeedManager;

        public FeedSynchronizer(
            IFeedManager feedManager, 
            IHtmlService htmlService, 
            IWebPagesService webPagesService,
            IMatchesProvider matchesProvider,
            IWebDriverFactory webDriverFactory,
            IUnprocessedFeedManager unprocessedFeedManager)
        {
            this.feedManager = feedManager;
            this.htmlService = htmlService;
            this.webPagesService = webPagesService;
            this.matchesProvider = matchesProvider;
            this.unprocessedFeedManager = unprocessedFeedManager;
            this.webDriver = webDriverFactory.CreateWebDriver(CommonConstants.FEED_PORT);
        }

        public void Sync()
        {
            webPagesService.Load(webDriver, CommonConstants.FEED_URL, CommonConstants.WAIT_FOR_SCROLL_CONTAINER);
            webPagesService.ScrollToBottom(webDriver);

            ICollection<MatchFeedModel> processedFeed = new List<MatchFeedModel>();
            IEnumerable<string> urls = htmlService.GetMatchUrls(MatchXPaths.EVENT_BODY, webDriver.PageSource);

            foreach (var url in urls)
            {
                bool isLoaded = webPagesService.Load(webDriver, url, CommonConstants.WAIT_FOR_MATCH_HEADER);

                if (isLoaded)
                {
                    HtmlNode matchContainer = htmlService.GetMatchContainer(ContainerXPaths.MATCH, webDriver.PageSource);
                    MatchFeedModel feedModel = matchesProvider.Get(matchContainer);
                    feedManager.Manage(feedModel);

                    processedFeed.Add(feedModel);
                }
            }

            unprocessedFeedManager.Manage(processedFeed);
        }

        public void Stop()
        {
            webDriver.Quit();
        }
    }
}