namespace SportsBetting.Handlers.Queries.Odds
{
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class OddByKeyQuery : IQuery<Odd>
    {
        public OddByKeyQuery(int key)
        {
            Key = key;
        }

        public int Key { get; set; }
    }
}