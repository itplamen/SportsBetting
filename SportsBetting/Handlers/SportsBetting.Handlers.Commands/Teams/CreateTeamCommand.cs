namespace SportsBetting.Handlers.Commands.Teams
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateTeamCommand : ICommand
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public int? Score { get; set; }

        public string SportId { get; set; }
    }
}