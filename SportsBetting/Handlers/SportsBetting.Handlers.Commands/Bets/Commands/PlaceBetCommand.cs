namespace SportsBetting.Handlers.Commands.Bets.Commands
{
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Contracts;

    public class PlaceBetCommand : ICommand, IMapTo<Bet>
    {
        public decimal Stake { get; set; }

        public string OddId { get; set; }

        public string Username { get; set; }
    }
}