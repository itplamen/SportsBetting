namespace SportsBetting.Handlers.Query.Contracts
{
    public interface IQueryHandler<TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}