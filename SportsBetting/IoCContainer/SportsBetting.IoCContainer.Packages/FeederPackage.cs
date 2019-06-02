namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Feeder.Core.Contracts.Managers;
    using SportsBetting.Feeder.Core.Contracts.Mappers;
    using SportsBetting.Feeder.Core.Managers;
    using SportsBetting.Feeder.Core.Mappers;
    using SportsBetting.Services.Feeder.Contracts.Factories;
    using SportsBetting.Services.Feeder.Contracts.Providers;
    using SportsBetting.Services.Feeder.Contracts.Services;
    using SportsBetting.Services.Feeder.Factories;
    using SportsBetting.Services.Feeder.Providers.Markets;
    using SportsBetting.Services.Feeder.Providers.Matches;
    using SportsBetting.Services.Feeder.Providers.Odds;
    using SportsBetting.Services.Feeder.Providers.Teams;
    using SportsBetting.Services.Feeder.Providers.Tournaments;
    using SportsBetting.Services.Feeder.Services;

    public sealed class FeederPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            RegisterMappers(container);
            RegisterManagers(container);
            RegisterFactories(container);
            RegisterFeederServices(container);
            RegisterProviders(container);
        }

        private void RegisterMappers(Container container)
        {
            container.Register(typeof(IMapper<,>), typeof(OddsMapper), Lifestyle.Singleton);
            container.Register(typeof(IMapper<,>), typeof(MatchesMapper), Lifestyle.Singleton);
        }

        private void RegisterManagers(Container container)
        {
            container.Register<ICategoriesManager, CategoriesManager>(Lifestyle.Singleton);
            container.Register<ITournamentsManager, TournamentsManager>(Lifestyle.Singleton);
            container.Register<ITeamsManager, TeamsManager>(Lifestyle.Singleton);
            container.Register<IMatchesManager, MatchesManager>(Lifestyle.Singleton);
            container.Register<IMarketsManager, MarketsManager>(Lifestyle.Singleton);
            container.Register<IOddsManager, OddsManager>(Lifestyle.Singleton);
            container.Register<IFeedManager, FeedManager>(Lifestyle.Singleton);
        }

        private void RegisterFactories(Container container)
        {
            container.Register<IWebDriverFactory, WebDriverFactory>(Lifestyle.Singleton);
            container.Register<IObjectFactory, ObjectFactory>(Lifestyle.Singleton);
        }

        private void RegisterFeederServices(Container container)
        {
            container.Register<IWebPagesService, WebPagesService>(Lifestyle.Singleton);
            container.Register<IHtmlService, HtmlService>(Lifestyle.Singleton);
        }

        private void RegisterProviders(Container container)
        {
            container.Register<IMarketsProvider, MarketsProvider>(Lifestyle.Singleton);
            container.Register<ITeamsProvider, TeamsProvider>(Lifestyle.Singleton);
            container.Register<ITournametsProvider, TournametsProvider>(Lifestyle.Singleton);
            container.Register<IMatchesProvider, MatchesProvider>(Lifestyle.Singleton);
            container.Register<IOddsProvider, HandicapOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, CorrectScoreOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, ThreeWayOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, TotalLineOddsProvider>(Lifestyle.Singleton);
            container.RegisterDecorator<IOddsProvider, TwoWayOddsProvider>(Lifestyle.Singleton);
        }
    }
}