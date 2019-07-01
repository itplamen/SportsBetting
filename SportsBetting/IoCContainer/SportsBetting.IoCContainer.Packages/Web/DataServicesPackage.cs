namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Services.Data;
    using SportsBetting.Services.Data.Contracts;
    using SportsBetting.Services.Utils;
    using SportsBetting.Services.Utils.Contracts;

    public sealed class DataServicesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IAccountsService, AccountsService>(new WebRequestLifestyle());
            container.Register<IEncryptionService, EncryptionService>(new WebRequestLifestyle());
            container.Register<ICategoriesService, CategoriesService>(new WebRequestLifestyle());
        }
    }
}