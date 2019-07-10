namespace SportsBetting.Handlers.Commands.Categories
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, string>
    {
        private readonly ICache<Category> categoriesCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateCategoryCommandHandler(ICache<Category> categoriesCache, ISportsBettingDbContext dbContext)
        {
            this.categoriesCache = categoriesCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateCategoryCommand command)
        {
            Category category = Mapper.Map<Category>(command);
            category.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Category>().InsertOne(category);
            categoriesCache.Add(category.Key, category);

            return category.Id;
        }
    }
}