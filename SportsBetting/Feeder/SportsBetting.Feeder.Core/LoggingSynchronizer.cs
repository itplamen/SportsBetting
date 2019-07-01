namespace SportsBetting.Feeder.Core
{
    using System;

    using SportsBetting.Feeder.Core.Contracts;
    
    public class LoggingSynchronizer : ISynchronizer
    {
        private readonly ISynchronizer decoratedSynchronizer;

        public LoggingSynchronizer(ISynchronizer decoratedSynchronizer)
        {
            this.decoratedSynchronizer = decoratedSynchronizer;
        }

        public void Sync()
        {
            try
            {
                decoratedSynchronizer.Sync();
            }
            catch (Exception ex)
            {
                // TODO: Log
            }
        }

        public void Stop()
        {
            decoratedSynchronizer.Stop();
        }   
    }
}