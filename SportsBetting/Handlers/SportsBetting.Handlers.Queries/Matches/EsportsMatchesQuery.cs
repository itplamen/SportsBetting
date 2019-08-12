namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;

    using SportsBetting.Handlers.Queries.Contracts;

    public class EsportsMatchesQuery : IQuery<IEnumerable<EsportsMatchesResult>>
    {
        public int Take { get; set; }

        public string Token { get; set; }
    }
}