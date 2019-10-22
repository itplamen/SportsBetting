namespace SportsBetting.Data.Initialize
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Data.Models.Base;

    public sealed class DbInitializer : IAplicationInitializer
    {
        private readonly ISportsBettingDbContext dbContext;

        public DbInitializer(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Init()
        {
            InitCollection<Account>();
            InitCollection<Authentication>();
            InitCollection<Bet>();
            InitCollection<Tournament>();
            InitCollection<Team>();
            InitCollection<Match>();
            InitCollection<Market>();
            InitCollection<Odd>();

            SeedData seedData = new SeedData();

            IMongoCollection<Sport> sports = InitCollection<Sport>();
            Seed(sports, seedData.Sports);
        }

        private IMongoCollection<TEntity> InitCollection<TEntity>()
            where TEntity : BaseModel
        {
            IMongoCollection<TEntity> collection = dbContext.GetCollection<TEntity>();
            CreatIndex(collection);

            return collection;
        }

        private void CreatIndex<TEntity>(IMongoCollection<TEntity> collection)
            where TEntity : BaseModel
        {
            CreateIndexModel<TEntity> indexModel = new CreateIndexModel<TEntity>(Builders<TEntity>.IndexKeys.Ascending(x => x.Key));
            collection.Indexes.CreateOne(indexModel);
        }

        private void Seed<TEntity>(IMongoCollection<TEntity> collection, IEnumerable<TEntity> data)
            where TEntity : BaseModel
        {
            if (!collection.Find(x => true).Any())
            {
                collection.InsertMany(data);
            }
        }
    }
}