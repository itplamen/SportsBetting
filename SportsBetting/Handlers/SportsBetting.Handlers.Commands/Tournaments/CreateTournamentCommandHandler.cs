namespace SportsBetting.Handlers.Commands.Tournaments
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class CreateTournamentCommandHandler : ICommandHandler<CreateTournamentCommand, string>
    {
        private readonly ICache<Tournament> tournamentsCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateTournamentCommandHandler(ICache<Tournament> tournamentsCache, ISportsBettingDbContext dbContext)
        {
            this.tournamentsCache = tournamentsCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateTournamentCommand command)
        {
            Tournament tournament = Mapper.Map<Tournament>(command);
            tournament.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Tournament>().InsertOne(tournament);
            tournamentsCache.Add(tournament.Key, tournament);

            return tournament.Id;
        }
    }
}