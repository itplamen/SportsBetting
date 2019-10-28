namespace SportsBetting.Handlers.Queries.Common.Queries
{
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntityByIdQuery<TEntity> : IQuery<TEntity>
        where TEntity : BaseModel
    {
        public EntityByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}