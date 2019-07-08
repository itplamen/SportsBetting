namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByIdQueryHandler<>), new WebRequestLifestyle());
            container.Register(typeof(IQueryHandler<>), typeof(AllEntitiesWithDeletedHandler<>), new WebRequestLifestyle());
        }
    }
}