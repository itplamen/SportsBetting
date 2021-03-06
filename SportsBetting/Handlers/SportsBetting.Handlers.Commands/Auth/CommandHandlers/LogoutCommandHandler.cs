﻿namespace SportsBetting.Handlers.Commands.Auth.CommandHandlers
{
    using System;

    using MongoDB.Driver;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Auth.Commands;
    using SportsBetting.Handlers.Commands.Contracts;

    public class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly ISportsBettingDbContext dbContext;

        public LogoutCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle(LogoutCommand command)
        {
            FilterDefinition<Authentication> filter = Builders<Authentication>.Filter
                .Eq(x => x.Id, command.Id);

            UpdateDefinition<Authentication> update = Builders<Authentication>.Update
               .Set(x => x.DeletedOn, DateTime.UtcNow)
               .Set(x => x.IsDeleted, true);

            dbContext.GetCollection<Authentication>().UpdateOne(filter, update);
        }
    }
}