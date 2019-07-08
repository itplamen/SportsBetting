namespace SportsBetting.IoCContainer.Packages.Web
{
    using System.Collections.Generic;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Categories;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public sealed class QueryHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IQueryHandler<IEnumerable<Category>>, AllCategoriesWithDeletedHandler>(new WebRequestLifestyle());
            container.Register(typeof(IQueryHandler<,>), typeof(EntitiesByIdQueryHandler<>), new WebRequestLifestyle());
        }
    }
}