namespace SportsBetting.Feeder.Models.Base
{
    public abstract class BaseFeedModel
    {
        public int Id => GenerateId();

        protected abstract int GenerateId();
    }
}