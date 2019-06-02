namespace SportsBetting.Feeder.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SimpleInjector.Packaging;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.IoCContainer;
    using SportsBetting.IoCContainer.Packages;

    public class FeederBootstrapper
    {
        private readonly static CancellationTokenSource source = new CancellationTokenSource();

        private readonly CancellationToken token;
        private readonly IPrematchSynchronizer prematchSynchronizer;

        public FeederBootstrapper()
        {
            InitializeDependencies();
            InitializeCaches();
            token = source.Token;
            prematchSynchronizer = SportsBettingContainer.Resolve<IPrematchSynchronizer>();
        }

        public void Start()
        {
            List<Action> actions = GetActions().ToList();
            actions.ForEach(async x => await Task.Run(x));
        }

        public void Stop()
        {
            source.Cancel();
            prematchSynchronizer.Stop();
        }

        private void InitializeDependencies()
        {
            IPackage[] packages = new IPackage[]
            {
                new DataCachePackage(),
                new FeederPackage()
            };

            SportsBettingContainer.Initialize(packages);
        }

        private void InitializeCaches()
        {
            ICacheInitializer cacheInitializer = SportsBettingContainer.Resolve<ICacheInitializer>();
            cacheInitializer.Init();
        }

        private IEnumerable<Action> GetActions()
        {
            return new List<Action>()
            {
                () => Synchronize(prematchSynchronizer)
            };
        }

        private void Synchronize(ISynchronizer synchronizer)
        {
            while (!token.IsCancellationRequested)
            {
                synchronizer.Sync();
            }
        }
    }
}