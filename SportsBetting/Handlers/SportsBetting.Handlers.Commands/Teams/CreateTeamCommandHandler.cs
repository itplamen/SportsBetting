namespace SportsBetting.Handlers.Commands.Teams
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public CreateTeamCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateTeamCommand command)
        {
            Team team = Mapper.Map<Team>(command);
            team.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Team>().InsertOne(team);

            return team.Id;
        }
    }
}