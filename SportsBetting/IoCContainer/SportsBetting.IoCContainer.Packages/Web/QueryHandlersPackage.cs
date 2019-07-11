namespace SportsBetting.IoCContainer.Packages.Web
{
    using System.Collections.Generic;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Accounts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Handlers.Queries.Matches;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByIdQueryHandler<>), new WebRequestLifestyle());
            container.Register(typeof(IQueryHandler<>), typeof(WithDeletedEntitiesHandler<>), new WebRequestLifestyle());
            container.Register<IQueryHandler<AccountByEmailQuery, Account>, AccountByEmailQueryHandler>(new WebRequestLifestyle());
            container.Register<IQueryHandler<AccountByUsernameQuery, Account>, AccountByUsernameQueryHandler>(new WebRequestLifestyle());
            container.Register<IQueryHandler<UpcomingMatchesQuery, IEnumerable<UpcomingMatchesResult>>, UpcomingMatchesQueryHandler>(new WebRequestLifestyle());
        }
    }
}