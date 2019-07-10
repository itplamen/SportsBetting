namespace SportsBetting.Handlers.Commands.Markets
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class CreateMarketCommandHandler : ICommandHandler<CreateMarketCommand, string>
    {
        private readonly ICache<Market> marketsCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateMarketCommandHandler(ICache<Market> marketsCache, ISportsBettingDbContext dbContext)
        {
            this.marketsCache = marketsCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateMarketCommand command)
        {
            Market market = Mapper.Map<Market>(command);
            market.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Market>().InsertOne(market);
            marketsCache.Add(market.Key, market);

            return market.Id;
        }
    }
}