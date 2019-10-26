namespace SportsBetting.Server.Api.Models.Bets
{
    using System.ComponentModel.DataAnnotations;

    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Handlers.Commands.Bets.Commands;

    public class BetRequestModel : IMapTo<PlaceBetCommand>
    {
        [Required(ErrorMessage = "{0} is required")]
        public decimal Stake { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string OddId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string AccountId { get; set; }
    }
}