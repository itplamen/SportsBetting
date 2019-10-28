namespace SportsBetting.Handlers.Queries.Matches.Queries
{
    using System.Collections.Generic;

    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AllMatchesQuery : IQuery<IEnumerable<MatchResult>>
    {
        public AllMatchesQuery(int take)
        {
            Take = take;
        }

        public int Take { get; set; }
    }
}