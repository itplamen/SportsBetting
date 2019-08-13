namespace SportsBetting.Handlers.Queries.Matches
{
    using System.Collections.Generic;

    using SportsBetting.Handlers.Queries.Contracts;

    public class EsportsMatchesQuery : IQuery<IEnumerable<EsportsMatchesResult>>
    {
        public EsportsMatchesQuery(int take)
        {
            Take = take;
        }

        public int Take { get; set; }
    }
}