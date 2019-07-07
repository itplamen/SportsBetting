namespace SportsBetting.Handlers.Commands.Matches
{
    using System;

    using AutoMapper;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class UpdateMatchCommandHandler : ICommandHandler<UpdateMatchCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public UpdateMatchCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(UpdateMatchCommand command)
        {
            FilterDefinition<Match> filter = Builders<Match>.Filter.Eq(x => x.Id, command.Id);

            Match match = Mapper.Map<Match>(command);
            match.ModifiedOn = DateTime.UtcNow;

            dbContext.GetCollection<Match>().ReplaceOne(filter, match);

            return match.Id;
        }
    }
}