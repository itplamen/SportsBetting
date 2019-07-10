namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Data;
    using SportsBetting.Data.Contracts;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISportsBettingDbContext, SportsBettingDbContext>(Lifestyle.Singleton);
        }
    }
}