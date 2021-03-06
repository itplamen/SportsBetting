﻿namespace SportsBetting.IoCContainer.Packages.Feeder
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Common.QueryHandlers;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Tournaments;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQueryDispatcher, QueryDispatcher>(Lifestyle.Singleton);
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByIdQueryHandler<>), Lifestyle.Singleton);
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByKeyQueryHandler<>), Lifestyle.Singleton);
            container.Register<IQueryHandler<TournamentByNameQuery, Tournament>, TournamentByNameQueryHandler>(Lifestyle.Singleton);
        }
    }
}