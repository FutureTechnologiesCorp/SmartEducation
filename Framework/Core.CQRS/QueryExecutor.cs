using System;
namespace Core.CQRS
{
    public class QueryExecutor<TQuery> : IQueryExecutor<TQuery>
    {
        TQuery _query;

        public QueryExecutor(TQuery query)
        {
            _query = query;
        }

        public TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func)
        {
            return func(_query);
        }
    }
}
