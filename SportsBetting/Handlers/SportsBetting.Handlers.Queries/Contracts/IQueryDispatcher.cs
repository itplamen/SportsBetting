namespace SportsBetting.Handlers.Queries.Contracts
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>();

        TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}