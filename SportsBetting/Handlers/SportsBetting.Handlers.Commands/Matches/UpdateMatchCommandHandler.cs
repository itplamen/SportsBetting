namespace SportsBetting.Handlers.Commands.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    using SportsBetting.Handlers.Queries.Common;
    using SportsBetting.Handlers.Queries.Contracts;

    public class UpdateMatchCommandHandler : ICommandHandler<UpdateMatchCommand, string>
    {
        private readonly ICache<Match> matchesCache;
        private readonly ISportsBettingDbContext dbContext;
        private readonly IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchByIdHandler;

        public UpdateMatchCommandHandler(
            ICache<Match> matchesCache, 
            ISportsBettingDbContext dbContext,
            IQueryHandler<EntitiesByIdQuery<Match>, IEnumerable<Match>> matchByIdHandler)
        {
            this.matchesCache = matchesCache;
            this.dbContext = dbContext;
            this.matchByIdHandler = matchByIdHandler;
        }

        public string Handle(UpdateMatchCommand command)
        {
            DateTime modifiedOn = DateTime.UtcNow;
            UpdateDocument(command, modifiedOn);

            Match match = UpdateCache(command, modifiedOn);

            return match.Id;
        }

        private void UpdateDocument(UpdateMatchCommand command, DateTime modifiedOn)
        {
            FilterDefinition<Match> filter = Builders<Match>.Filter.Eq(x => x.Id, command.Id);
            UpdateDefinition<Match> update = Builders<Match>.Update
               .Set(x => x.StartTime, command.StartTime)
               .Set(x => x.Score, command.Score)
               .Set(x => x.StreamURL, command.StreamURL)
               .Set(x => x.ModifiedOn, modifiedOn);

            dbContext.GetCollection<Match>().UpdateOne(filter, update);
        }

        private Match UpdateCache(UpdateMatchCommand command, DateTime modifiedOn)
        {
            Match match = GetMatch(command.Id);
            match.StartTime = command.StartTime;
            match.Score = command.Score;
            match.StreamURL = command.StreamURL;
            match.ModifiedOn = modifiedOn;

            matchesCache.Update(match.Key, match);

            return match;
        }

        private Match GetMatch(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Match> matchByIdQuery = new EntitiesByIdQuery<Match>(ids);
            Match match = matchByIdHandler.Handle(matchByIdQuery).FirstOrDefault();

            return match;
        }
    }
}