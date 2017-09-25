using System;
using FT.DataAccess;

namespace FT.CQRS
{
    public class Executor : IExecutor
    {
        readonly BaseDbContext _context;

        public Executor(BaseDbContext context)
        {
            _context = context;
        }
        
        public ICommandExecutor<TCommand> GetCommand<TCommand>()
        {
            var command = (TCommand)Activator.CreateInstance(typeof(TCommand));
            return new CommandExecutor<TCommand>(command, _context);
        }

        public IQueryExecutor<TQuery> GetQuery<TQuery>()
        {
            var query = (TQuery)Activator.CreateInstance(typeof(TQuery));
            return new QueryExecutor<TQuery>(query);
        }

        public ICommandChainExecutor CommandChain()
        {
            return new CommandChainExecutor(_context);
        }
    }
}
