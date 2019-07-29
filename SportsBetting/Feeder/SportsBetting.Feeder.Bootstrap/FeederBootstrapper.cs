namespace SportsBetting.Feeder.Bootstrap
{
    using System.Reflection;
    using SimpleInjector;
    using SimpleInjector.Packaging;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.IoCContainer.Packages.Common;
    using SportsBetting.IoCContainer.Packages.Feeder;

    public class FeederBootstrapper
    {
        private readonly Container container;
        private readonly ISynchronizer synchronizer;

        public FeederBootstrapper()
        {
            container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;

            synchronizer = container.GetInstance<ISynchronizer>();

            InitializeDependencies();
            InitializeDb();
            InitializeCaches();
            InitializeMapping();
        }

        public void Start()
        {
            synchronizer.Sync();
        }

        public void Stop()
        {
            synchronizer.Stop();
        }

        private void InitializeDependencies()
        {
            IPackage[] packages = new IPackage[]
            {
                new DataPackage(),
                new DataCachePackage(),
                new FeederPackage(),
                new QueryHandlersPackage(),
                new CommandHandlersPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            container.Verify();
        }

        private void InitializeDb()
        {
            IAplicationInitializer aplicationInitializer = container.GetInstance<IAplicationInitializer>();
            aplicationInitializer.Init();
        }

        private void InitializeCaches()
        {
            ICacheInitializer cacheInitializer = container.GetInstance<ICacheInitializer>();
            cacheInitializer.Init();
        }

        private void InitializeMapping()
        {
            const string MAPPING_ASSEMBLY = "SportsBetting.Handlers.Commands";
            AutoMapperConfig.RegisterMappings(Assembly.Load(MAPPING_ASSEMBLY));
        }
    }
}