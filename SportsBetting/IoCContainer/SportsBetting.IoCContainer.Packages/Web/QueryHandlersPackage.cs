namespace SportsBetting.IoCContainer.Packages.Web
{
    using System.Collections.Generic;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Common.QueryHandlers;
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Markets.Queries;
    using SportsBetting.Handlers.Queries.Markets.QueryHandlers;
    using SportsBetting.Handlers.Queries.Matches.Queries;
    using SportsBetting.Handlers.Queries.Matches.QueryHandlers;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQueryDispatcher, QueryDispatcher>(new WebRequestLifestyle());
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByIdQueryHandler<>), new WebRequestLifestyle());
            container.Register(typeof(IQueryHandler<>), typeof(WithDeletedEntitiesHandler<>), new WebRequestLifestyle());
            container.Register<IQueryHandler<MatchByIdQuery, MatchResult>, MatchByIdQueryHandler>(new WebRequestLifestyle());
            container.Register<IQueryHandler<AllMatchesQuery, IEnumerable<MatchResult>>, AllMatchesQueryHandler>(new WebRequestLifestyle());
            container.Register<IQueryHandler<AccountByUsernameQuery, Account>, AccountByUsernameQueryHandler>(new WebRequestLifestyle());
            container.Register<IQueryHandler<MarketsByMatchIdQuery, IEnumerable<Market>>, MarketsByMatchIdQueryHandler>(new WebRequestLifestyle());
        }
    }
}