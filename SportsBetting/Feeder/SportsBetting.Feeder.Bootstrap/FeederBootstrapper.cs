namespace SportsBetting.Feeder.Bootstrap
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

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
        private readonly static CancellationTokenSource source = new CancellationTokenSource();

        private readonly CancellationToken token;
        private readonly ICacheLoader cacheLoader;
        private readonly ISynchronizer synchronizer;

        public FeederBootstrapper()
        {
            Container container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;

            InitializeDependencies(container);
            InitializeDb(container);
            InitializeMapping();

            this.cacheLoader = container.GetInstance<ICacheLoader>();
            this.cacheLoader.Init();

            this.token = source.Token;
            this.synchronizer = container.GetInstance<ISynchronizer>();
        }

        public void Start()
        {
            Task task = new Task(() => RefreshCaches(cacheLoader));
            task.Start();

            synchronizer.Sync();
        }

        public void Stop()
        {
            source.Cancel();

            synchronizer.Stop();
        }

        private void InitializeDependencies(Container container)
        {
            IPackage[] packages = new IPackage[]
            {
                new DataPackage(),
                new DataCachePackage(),
                new FeederPackage(),
                new CommonPackage(),
                new QueryHandlersPackage(),
                new CommandHandlersPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            container.Verify();
        }

        private void InitializeDb(Container container)
        {
            IAplicationInitializer aplicationInitializer = container.GetInstance<IAplicationInitializer>();
            aplicationInitializer.Init();
        }

        private void InitializeMapping()
        {
            const string MAPPING_ASSEMBLY = "SportsBetting.Handlers.Commands";
            AutoMapperConfig.RegisterMappings(Assembly.Load(MAPPING_ASSEMBLY));
        }

        private void RefreshCaches(ICacheLoader cacheLoader)
        {
            while (!token.IsCancellationRequested)
            {
                cacheLoader.Refresh();
            }
        }
    }
}