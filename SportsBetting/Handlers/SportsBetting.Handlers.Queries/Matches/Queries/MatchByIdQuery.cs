namespace SportsBetting.Handlers.Queries.Matches.Queries
{
    using SportsBetting.Handlers.Queries.Common.Results;
    using SportsBetting.Handlers.Queries.Contracts;

    public class MatchByIdQuery : IQuery<MatchResult>
    {
        public MatchByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}