namespace SportsBetting.Handlers.Queries.Teams
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class TeamByKeyQuery : IQuery<Team>
    {
        public TeamByKeyQuery(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }
}