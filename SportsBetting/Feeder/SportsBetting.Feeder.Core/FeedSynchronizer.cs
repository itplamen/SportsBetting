namespace SportsBetting.Feeder.Core
{
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using OpenQA.Selenium.Remote;

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

        public FeedSynchronizer(
            IFeedManager feedManager, 
            IHtmlService htmlService, 
            IWebPagesService webPagesService,
            IMatchesProvider matchesProvider,
            IWebDriverFactory webDriverFactory)
        {
            this.feedManager = feedManager;
            this.htmlService = htmlService;
            this.webPagesService = webPagesService;
            this.matchesProvider = matchesProvider;
            this.webDriver = webDriverFactory.CreateWebDriver(7772);
        }

        public void Sync()
        {
            webPagesService.Load(webDriver, "https://gg.bet/en/betting", "ScrollToTop__container___37xDi");
            webPagesService.ScrollToBottom(webDriver);

            IEnumerable<string> urls = htmlService.GetMatchUrls(".//div[contains(@class, 'sportEventRow__body___3Ywcg')]", webDriver.PageSource);

            foreach (var url in urls)
            {
                bool isLoaded = webPagesService.Load(webDriver, url, "MatchHeaderInfobox__info-box___2rQ4p");

                if (isLoaded)
                {
                    HtmlNode matchContainer = htmlService.GetMatchContainer(".//div[starts-with(@class,'Match__container')]", webDriver.PageSource);
                    MatchFeedModel feedModel = matchesProvider.Get(matchContainer);

                    feedManager.Manage(feedModel);
                }
            }
        }

        public void Stop()
        {
            webDriver.Quit();
        }
    }
}