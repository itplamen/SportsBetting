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
            InitCollection<Category>();
            InitCollection<Tournament>();
            InitCollection<Team>();
            InitCollection<Match>();
            InitCollection<Market>();
            InitCollection<Odd>();

            SeedData seedData = new SeedData();

            IMongoCollection<Sport> sports = InitCollection<Sport>();
            Seed(sports, seedData.Sports);
        }

        private IMongoCollection<T> InitCollection<T>()
            where T : BaseModel
        {
            IMongoCollection<T> collection = dbContext.GetCollection<T>(typeof(T).Name);
            CreatIndex(collection);

            return collection;
        }

        private void CreatIndex<T>(IMongoCollection<T> collection)
            where T : BaseModel
        {
            CreateIndexModel<T> indexModel = new CreateIndexModel<T>(Builders<T>.IndexKeys.Ascending(x => x.Key));
            collection.Indexes.CreateOne(indexModel);
        }

        private void Seed<T>(IMongoCollection<T> collection, IEnumerable<T> data)
            where T : BaseModel
        {
            if (!collection.Find(x => true).Any())
            {
                collection.InsertMany(data);
            }
        }
    }
}