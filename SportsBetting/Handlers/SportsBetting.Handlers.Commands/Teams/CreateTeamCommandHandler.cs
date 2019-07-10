namespace SportsBetting.Handlers.Commands.Teams
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;
    
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, string>
    {
        private readonly ICache<Team> teamsCache;
        private readonly ISportsBettingDbContext dbContext;

        public CreateTeamCommandHandler(ICache<Team> teamsCache, ISportsBettingDbContext dbContext)
        {
            this.teamsCache = teamsCache;
            this.dbContext = dbContext;
        }

        public string Handle(CreateTeamCommand command)
        {
            Team team = Mapper.Map<Team>(command);
            team.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Team>().InsertOne(team);
            teamsCache.Add(team.Key, team);

            return team.Id;
        }
    }
}