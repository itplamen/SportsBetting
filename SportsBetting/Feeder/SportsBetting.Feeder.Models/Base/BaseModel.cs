namespace SportsBetting.Feeder.Models.Base
{
    public abstract class BaseModel
    {
        public int Id => GenerateId();

        protected abstract int GenerateId();
    }
}