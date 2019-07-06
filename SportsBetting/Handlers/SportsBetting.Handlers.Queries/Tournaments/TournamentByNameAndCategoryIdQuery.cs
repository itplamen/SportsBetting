namespace SportsBetting.Handlers.Queries.Tournaments
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TournamentByNameAndCategoryIdQuery : IQuery<Tournament>
    {
        public TournamentByNameAndCategoryIdQuery(string name, string categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        public string Name { get; set; }

        public string CategoryId { get; set; }
    }
}