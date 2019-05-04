namespace SportsBetting.Data.Initialize
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Models;
    using SportsBetting.Data.Models.Base;

    public sealed class DbConfig
    {
        public static void Initialize(SportsBettingDbContext context)
        {
            InitCollection<Category>(context);
            InitCollection<Tournament>(context);
            InitCollection<Team>(context);
            InitCollection<Match>(context);
            InitCollection<Market>(context);
            InitCollection<Odd>(context);

            SeedData seedData = new SeedData();

            IMongoCollection<Sport> sports = InitCollection<Sport>(context);
            Seed(sports, seedData.Sports);
        }

        private static IMongoCollection<T> InitCollection<T>(SportsBettingDbContext context)
            where T : BaseModel
        {
            IMongoCollection<T> collection = context.GetCollection<T>(typeof(T).Name);
            CreatIndex(collection);

            return collection;
        }

        private static void CreatIndex<T>(IMongoCollection<T> collection)
            where T : BaseModel
        {
            CreateIndexModel<T> indexModel = new CreateIndexModel<T>(Builders<T>.IndexKeys.Ascending(x => x.Key));
            collection.Indexes.CreateOne(indexModel);
        }

        private static void Seed<T>(IMongoCollection<T> collection, IEnumerable<T> data)
            where T : BaseModel
        {
            if (!collection.Find(x => true).Any())
            {
                collection.InsertMany(data);
            }
        }
    }
}
