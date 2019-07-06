namespace SportsBetting.Handlers.Commands.Tournaments
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class CreateTournamentCommandHandler : ICommandHandler<CreateTournamentCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public CreateTournamentCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateTournamentCommand command)
        {
            Tournament tournament = Mapper.Map<Tournament>(command);
            tournament.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Tournament>().InsertOne(tournament);

            return tournament.Id;
        }
    }
}