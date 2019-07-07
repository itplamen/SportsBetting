namespace SportsBetting.Handlers.Commands.Odds
{
    using System;

    using AutoMapper;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class UpdateOddCommandHandler : ICommandHandler<UpdateOddCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public UpdateOddCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(UpdateOddCommand command)
        {
            FilterDefinition<Odd> filter = Builders<Odd>.Filter.Eq(x => x.Id, command.Id);

            Odd odd = Mapper.Map<Odd>(command);
            odd.ModifiedOn = DateTime.UtcNow;

            dbContext.GetCollection<Odd>().ReplaceOne(filter, odd);

            return odd.Id;
        }
    }
}