using System;
using Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CQRS
{
    public class Executor : IExecutor
    {
        readonly BaseDbContext _context;
        readonly IServiceProvider _serviceProvider;

        public Executor(BaseDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        
        public ICommandExecutor<TCommand> GetCommand<TCommand>()
        {
            var command = _serviceProvider.GetService<TCommand>();
            return new CommandExecutor<TCommand>(command, _context);
        }

        public IQueryExecutor<TQuery> GetQuery<TQuery>()
        {
            var query = _serviceProvider.GetService<TQuery>();
            return new QueryExecutor<TQuery>(query);
        }

        public ICommandChainExecutor CommandChain()
        {
            return new CommandChainExecutor(_context);
        }
    }
}
