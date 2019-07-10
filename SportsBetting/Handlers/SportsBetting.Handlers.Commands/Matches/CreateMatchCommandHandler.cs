namespace SportsBetting.Handlers.Commands.Matches
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, string>
    {
        private readonly ICache<Match> matchesCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateMatchCommandHandler(ICache<Match> matchesCache, ISportsBettingDbContext dbContext)
        {
            this.matchesCache = matchesCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateMatchCommand command)
        {
            Match match = Mapper.Map<Match>(command);
            match.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Match>().InsertOne(match);
            matchesCache.Add(match.Key, match);

            return match.Id;
        }
    }
}