using System;
namespace FT.DataAccess.CQRS
{
    public class Executor : IExecutor
    {
        public ICommandExecutor<TCommand> GetCommand<TCommand>()
        {
            var command = (TCommand)Activator.CreateInstance(typeof(TCommand));
            return new CommandExecutor<TCommand>(command);
        }

        public IQueryExecutor<TQuery> GetQuery<TQuery>()
        {
            var query = (TQuery)Activator.CreateInstance(typeof(TQuery));
            return new QueryExecutor<TQuery>(query);
        }
    }
}
