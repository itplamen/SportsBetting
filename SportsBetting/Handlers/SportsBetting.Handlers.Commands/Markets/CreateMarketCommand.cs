namespace SportsBetting.Handlers.Commands.Markets
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateMarketCommand : ICommand
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string MatchId { get; set; }
    }
}