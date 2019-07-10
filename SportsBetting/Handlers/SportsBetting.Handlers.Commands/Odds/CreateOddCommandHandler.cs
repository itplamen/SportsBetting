namespace SportsBetting.Handlers.Commands.Odds
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateOddCommandHandler : ICommandHandler<CreateOddCommand, string>
    {
        private readonly ICache<Odd> oddsCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateOddCommandHandler(ICache<Odd> oddsCache, ISportsBettingDbContext dbContext)
        {
            this.oddsCache = oddsCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateOddCommand command)
        {
            Odd odd = Mapper.Map<Odd>(command);
            odd.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Odd>().InsertOne(odd);
            oddsCache.Add(odd.Key, odd);

            return odd.Id;
        }
    }
}