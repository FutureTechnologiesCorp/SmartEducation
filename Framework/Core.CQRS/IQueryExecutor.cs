using System;
namespace Core.CQRS
{
    public interface IQueryExecutor<TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func);
    }
}
