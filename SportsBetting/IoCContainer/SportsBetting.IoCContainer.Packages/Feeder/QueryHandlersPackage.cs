namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQueryHandler<MarketByKeyQuery, Market>, MarketByKeyQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<CategoryByNameQuery, Category>, CategoryByNameQueryHandler>(Lifestyle.Singleton);
        }
    }
}