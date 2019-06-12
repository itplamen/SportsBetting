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
            container.Register<IMatchesService, MatchesService>(Lifestyle.Singleton);
        }
    }
}