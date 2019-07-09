namespace SportsBetting.Feeder.Models.Base
{
    public abstract class BaseFeedModel
    {
        public int Key => GenerateKey();

        protected abstract int GenerateKey();
    }
}