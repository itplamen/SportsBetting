namespace SportsBetting.Handlers.Commands.Odds
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Feeder.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateOddCommand : ICommand, IMapFrom<OddFeedModel>, IMapTo<Odd>
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Header { get; set; }

        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }

        public int Rank { get; set; }

        public OddResultStatus ResultStatus { get; set; }

        public string MarketId { get; set; }

        public string MatchId { get; set; }
    }
}