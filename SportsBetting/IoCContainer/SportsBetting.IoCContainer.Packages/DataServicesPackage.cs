namespace SportsBetting.IoCContainer.Packages
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Services.Data;
    using SportsBetting.Services.Data.Contracts;

    public class DataServicesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICategoriesService, CategoriesService>(Lifestyle.Singleton);
            container.Register<IMatchesService, MatchesService>(Lifestyle.Singleton);
            container.Register<IMarketsService, MarketsService>(Lifestyle.Singleton);
            container.Register<IOddsService, OddsService>(Lifestyle.Singleton);
            container.Register<ITeamsService, TeamsService>(Lifestyle.Singleton);
            container.Register<ITournamentsService, TournamentsService>(Lifestyle.Singleton);
        }
    }
}