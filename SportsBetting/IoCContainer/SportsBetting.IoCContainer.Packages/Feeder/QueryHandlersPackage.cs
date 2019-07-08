﻿namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets;
    using SportsBetting.Handlers.Queries.Matches;
    using SportsBetting.Handlers.Queries.Odds;
    using SportsBetting.Handlers.Queries.Teams;
    using SportsBetting.Handlers.Queries.Tournaments;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQueryHandler<OddByKeyQuery, Odd>, OddByKeyQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<TeamByKeyQuery, Team>, TeamByKeyQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<MatchByKeyQuery, Match>, MatchByKeyQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<MarketByKeyQuery, Market>, MarketByKeyQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<CategoryByNameQuery, Category>, CategoryByNameQueryHandler>(Lifestyle.Singleton);
            container.Register<IQueryHandler<TournamentByNameAndCategoryIdQuery, Tournament>, TournamentByNameAndCategoryIdQueryHandler>(Lifestyle.Singleton);
        }
    }
}