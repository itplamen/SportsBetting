namespace SportsBetting.Handlers.Queries.Tournaments
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TournamentByNameQuery : IQuery<Tournament>
    {
        public TournamentByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}