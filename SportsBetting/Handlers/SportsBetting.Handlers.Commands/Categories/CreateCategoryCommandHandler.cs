namespace SportsBetting.Handlers.Commands.Categories
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public CreateCategoryCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateCategoryCommand command)
        {
            Category category = Mapper.Map<Category>(command);
            category.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Category>().InsertOne(category);

            return category.Id;
        }
    }
}