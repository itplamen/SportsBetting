namespace SportsBetting.Feeder.Core.Contracts
{
    public interface ISynchronizer
    {
        void Sync();

        void Stop();
    }
}