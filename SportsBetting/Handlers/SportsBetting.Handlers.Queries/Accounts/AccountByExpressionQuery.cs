namespace SportsBetting.Handlers.Queries.Accounts
{
    using System;
    using System.Linq.Expressions;

    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;

    public class AccountByExpressionQuery : IQuery<Account>
    {
        public AccountByExpressionQuery(Expression<Func<Account, bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<Account, bool>> Expression { get; set; }
    }
}