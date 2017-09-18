using System;
namespace FT.CQRS
{
    public interface IQueryExecutor<TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func);
    }
}
