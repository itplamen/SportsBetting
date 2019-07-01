namespace SportsBetting.Feeder.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SportsBetting.Feeder.Core.Contracts;

    public class SynchronizerComposite : ISynchronizer
    {
        private readonly static CancellationTokenSource source = new CancellationTokenSource();

        private readonly CancellationToken token;
        private readonly IEnumerable<ISynchronizer> synchronizers;
        private List<Task> tasks;

        public SynchronizerComposite(IEnumerable<ISynchronizer> synchronizers)
        {
            this.synchronizers = synchronizers;
        }

        public void Sync()
        {
            tasks = synchronizers.Select(x => new Task(() => Synchronize(x))).ToList();
            tasks.ForEach(x => x.Start());
        }

        public void Stop()
        {
            source.Cancel();
            Task.WaitAll(tasks.ToArray());

            synchronizers.ToList().ForEach(x => x.Stop());
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