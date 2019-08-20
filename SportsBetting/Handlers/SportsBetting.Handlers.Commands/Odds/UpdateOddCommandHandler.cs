namespace SportsBetting.Handlers.Commands.Odds
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

    public class UpdateOddCommandHandler : ICommandHandler<UpdateOddCommand, string>
    {
        private readonly ICache<Odd> oddsCache;
        private readonly ISportsBettingDbContext dbContext;
        private readonly IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddByIdHandler;

        public UpdateOddCommandHandler(
            ICache<Odd> oddsCache, 
            ISportsBettingDbContext dbContext,
            IQueryHandler<EntitiesByIdQuery<Odd>, IEnumerable<Odd>> oddByIdHandler)
        {
            this.oddsCache = oddsCache;
            this.dbContext = dbContext;
            this.oddByIdHandler = oddByIdHandler;
        }

        public string Handle(UpdateOddCommand command)
        {
            DateTime modifiedOn = DateTime.UtcNow;
            UpdateDocument(command, modifiedOn);

            Odd odd = UpdateCache(command, modifiedOn);

            return odd.Id;
        }

        private void UpdateDocument(UpdateOddCommand command, DateTime modifiedOn)
        {
            FilterDefinition<Odd> filter = Builders<Odd>.Filter.Eq(x => x.Id, command.Id);
            UpdateDefinition<Odd> update = Builders<Odd>.Update
               .Set(x => x.Value, command.Value)
               .Set(x => x.IsActive, command.IsActive)
               .Set(x => x.IsSuspended, command.IsSuspended)
               .Set(x => x.ResultStatus, command.ResultStatus)
               .Set(x => x.ModifiedOn, modifiedOn);

            dbContext.GetCollection<Odd>().UpdateOne(filter, update);
        }

        private Odd UpdateCache(UpdateOddCommand command, DateTime modifiedOn)
        {
            Odd odd = GetOdd(command.Id);
            odd.Value = command.Value;
            odd.IsActive = command.IsActive;
            odd.IsSuspended = command.IsSuspended;
            odd.ResultStatus = command.ResultStatus;
            odd.ModifiedOn = modifiedOn;

            oddsCache.Update(odd.Key, odd);

            return odd;
        }

        private Odd GetOdd(string id)
        {
            IEnumerable<string> ids = new List<string>() { id };
            EntitiesByIdQuery<Odd> oddByIdQuery = new EntitiesByIdQuery<Odd>(ids);
            Odd odd = oddByIdHandler.Handle(oddByIdQuery).FirstOrDefault();

            return odd;
        }
    }
}