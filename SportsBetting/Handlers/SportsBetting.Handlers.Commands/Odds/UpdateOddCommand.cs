namespace SportsBetting.Handlers.Commands.Odds
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class UpdateOddCommand : ICommand, IMapFrom<OddFeedModel>, IMapTo<Odd>
    {
        public string Id { get; set; }

        public decimal Value { get; set; }

        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }

        public OddResultStatus ResultStatus { get; set; }
    }
}