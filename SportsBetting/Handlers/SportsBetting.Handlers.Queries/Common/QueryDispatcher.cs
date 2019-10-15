namespace SportsBetting.Handlers.Queries.Common
{
    using SimpleInjector;

    using SportsBetting.Handlers.Queries.Contracts;

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Container container;

        public QueryDispatcher(Container container)
        {
            this.container = container;
        }

        public TResult Dispatch<TResult>()
        {
            IQueryHandler<TResult> handler = container.GetInstance<IQueryHandler<TResult>>();

            return handler.Handle();
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery<TResult>
        {
            IQueryHandler<TQuery, TResult> handler = container.GetInstance<IQueryHandler<TQuery, TResult>>();

            return handler.Handle(query);
        }
    }
}