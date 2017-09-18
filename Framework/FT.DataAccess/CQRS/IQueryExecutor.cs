using System;
namespace FT.DataAccess.CQRS
{
    public interface IQueryExecutor<TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func);
    }
}
