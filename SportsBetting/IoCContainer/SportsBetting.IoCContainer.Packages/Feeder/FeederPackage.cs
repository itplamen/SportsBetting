﻿namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Feeder.Core;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.Feeder.Core.Contracts.Factories;
    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Core.Contracts.Providers;
    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Factories;
    using SportsBetting.Feeder.Core.Managers;
    using SportsBetting.Feeder.Core.Providers.Markets;
    using SportsBetting.Feeder.Core.Providers.Matches;
    using SportsBetting.Feeder.Core.Providers.Odds;
    using SportsBetting.Feeder.Core.Providers.Teams;
    using SportsBetting.Feeder.Core.Providers.Tournaments;
    using SportsBetting.Feeder.Core.Services;

    public sealed class FeederPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            RegisterManagers(container);
            RegisterFactories(container);
            RegisterFeederServices(container);
            RegisterProviders(container);
            RegisterSynchronizers(container);
        }

        private void RegisterManagers(Container container)
        {
            container.Register<ITournamentsManager, TournamentsManager>(Lifestyle.Singleton);
            container.Register<ITeamsManager, TeamsManager>(Lifestyle.Singleton);
            container.Register<IMatchesManager, MatchesManager>(Lifestyle.Singleton);
            container.Register<IMarketsManager, MarketsManager>(Lifestyle.Singleton);
            container.Register<IOddsManager, OddsManager>(Lifestyle.Singleton);
            container.Register<IFeedManager, FeedManager>(Lifestyle.Singleton);
            container.Register<IUnprocessedFeedManager, UnprocessedFeedManager>(Lifestyle.Singleton);
        }

        private void RegisterFactories(Container container)
        {
            container.Register<IWebDriverFactory, WebDriverFactory>(Lifestyle.Singleton);
        }

        private void RegisterFeederServices(Container container)
        {
            container.Register(typeof(IWebPagesService<>), typeof(WebPagesService<>), Lifestyle.Singleton);
            container.Register<IHtmlService, HtmlService>(Lifestyle.Singleton);
        }

        private void RegisterProviders(Container container)
        {
            container.Register<IMarketsProvider, MarketsProvider>(Lifestyle.Singleton);
            container.Register<ITeamsProvider, TeamsProvider>(Lifestyle.Singleton);
            container.Register<ITournamentsProvider, TournamentsProvider>(Lifestyle.Singleton);
            container.Register<IMatchesProvider, MatchesProvider>(Lifestyle.Singleton);
            container.Register<IOddsProvider, HandicapOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, CorrectScoreOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, ThreeWayOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, TotalLineOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, TwoWayOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, LoggingOddsProvider>(Lifestyle.Singleton);
        }

        private void RegisterSynchronizers(Container container)
        {
            container.Register<ISynchronizer, SynchronizerComposite>(Lifestyle.Singleton);
            container.RegisterDecorator<ISynchronizer, LoggingSynchronizer>(Lifestyle.Singleton);
            container.Collection.Append<ISynchronizer, FeedSynchronizer>(Lifestyle.Singleton);
        }
    }
}