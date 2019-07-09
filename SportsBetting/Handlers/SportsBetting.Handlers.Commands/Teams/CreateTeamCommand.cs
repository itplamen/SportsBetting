namespace SportsBetting.Handlers.Commands.Teams
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateTeamCommand : ICommand, IMapFrom<TeamFeedModel>, IMapTo<Team>
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string SportId { get; set; }
    }
}