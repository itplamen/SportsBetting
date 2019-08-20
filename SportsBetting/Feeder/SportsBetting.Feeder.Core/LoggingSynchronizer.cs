namespace SportsBetting.Feeder.Core
{
    using System;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Feeder.Core.Contracts;
    
    public class LoggingSynchronizer : ISynchronizer
    {
        private const string LOGGER = nameof(LoggingSynchronizer);

        private readonly ILogger logger;
        private readonly string synchronizerName;
        private readonly ISynchronizer decoratedSynchronizer;

        public LoggingSynchronizer(ISynchronizer decoratedSynchronizer, ILoggerFactory loggerFactory)
        {
            this.decoratedSynchronizer = decoratedSynchronizer;
            this.synchronizerName = decoratedSynchronizer.GetType().Name;
            this.logger = loggerFactory.Create($"{logger}.txt", "Synchronizers");
        }

        public void Sync()
        {
            try
            {
                decoratedSynchronizer.Sync();
            }
            catch (Exception ex)
            {
                logger.LogError($"{synchronizerName} failed", ex);
            }
        }

        public void Stop()
        {
            decoratedSynchronizer.Stop();
        }   
    }
}