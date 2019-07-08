namespace SportsBetting.Handlers.Queries.Common
{
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntityByKeyQuery<TEntity> : IQuery<TEntity>
        where TEntity : BaseModel
    {
        public EntityByKeyQuery(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }
}