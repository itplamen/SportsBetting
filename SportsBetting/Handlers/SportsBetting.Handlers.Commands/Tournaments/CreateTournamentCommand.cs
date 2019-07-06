namespace SportsBetting.Handlers.Commands.Tournaments
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateTournamentCommand : ICommand
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }
    }
}