namespace SportsBetting.Handlers.Commands.Tournaments
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateTournamentCommand : ICommand, IMapFrom<TournamentFeedModel>, IMapTo<Tournament>
    {
        public int Key { get; set; }

        public string Name { get; set; }
    }
}