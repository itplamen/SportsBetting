namespace SportsBetting.Handlers.Commands.Odds
{
    using System;

    using AutoMapper;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class UpdateOddCommandHandler : ICommandHandler<UpdateOddCommand, string>
    {
        private readonly ICache<Odd> oddsCache;
        private readonly ISportsBettingDbContext dbContext;

        public UpdateOddCommandHandler(ICache<Odd> oddsCache, ISportsBettingDbContext dbContext)
        {
            this.oddsCache = oddsCache;
            this.dbContext = dbContext;
        }

        public string Handle(UpdateOddCommand command)
        {
            FilterDefinition<Odd> filter = Builders<Odd>.Filter.Eq(x => x.Id, command.Id);

            Odd odd = Mapper.Map<Odd>(command);
            odd.ModifiedOn = DateTime.UtcNow;

            dbContext.GetCollection<Odd>().ReplaceOne(filter, odd);
            oddsCache.Update(odd.Key, odd);

            return odd.Id;
        }
    }
}