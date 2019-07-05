namespace SportsBetting.Data
{
    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models.Base;

    public class SportsBettingDbContext : ISportsBettingDbContext
    {
        private const string DATABASE_HOST = "mongodb://127.0.0.1";
        private const string DATABASE_NAME = "SportsBetting";

        private readonly IMongoDatabase database;

        public SportsBettingDbContext()
        {
            this.database = GetDataBase();
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
             where TEntity : BaseModel
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        private IMongoDatabase GetDataBase()
        {
            IMongoClient mongoClient = new MongoClient(DATABASE_HOST);
            IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME);

            return database;
        }
    }
}