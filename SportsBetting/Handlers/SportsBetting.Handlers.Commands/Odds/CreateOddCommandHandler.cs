namespace SportsBetting.Handlers.Commands.Odds
{
    using System;

    using AutoMapper;

    using SportsBetting.Data.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateOddCommandHandler : ICommandHandler<CreateOddCommand, string>
    {
        private readonly ISportsBettingDbContext dbContext;

        public CreateOddCommandHandler(ISportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Handle(CreateOddCommand command)
        {
            Odd odd = Mapper.Map<Odd>(command);
            odd.CreatedOn = DateTime.UtcNow;

            dbContext.GetCollection<Odd>().InsertOne(odd);

            return odd.Id;
        }
    }
}