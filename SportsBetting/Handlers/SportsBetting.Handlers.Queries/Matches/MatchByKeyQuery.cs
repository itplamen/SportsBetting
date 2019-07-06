namespace SportsBetting.Handlers.Queries.Matches
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchByKeyQuery : IQuery<Match>
    {
        public MatchByKeyQuery(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }
}