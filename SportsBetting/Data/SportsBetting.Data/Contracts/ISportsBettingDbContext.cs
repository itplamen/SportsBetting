namespace SportsBetting.Data.Contracts
{
    using MongoDB.Driver;

    using SportsBetting.Data.Models.Base;

    public interface ISportsBettingDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name)
             where T : BaseModel;
    }
}