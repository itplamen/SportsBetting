namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Services.Utils;
    using SportsBetting.Services.Utils.Contracts;

    public sealed class DataServicesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IEncryptionService, EncryptionService>(new WebRequestLifestyle());
        }
    }
}