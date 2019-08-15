namespace SportsBetting.Data.Cache.Contracts
{
    public interface ICacheLoader
    {
        void Init();

        void Refresh();
    }
}