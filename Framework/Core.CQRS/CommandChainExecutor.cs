using System;
using Core.DataAccess;
using System.Collections.Generic;

namespace Core.CQRS
{
    public class CommandChainExecutor : ICommandChainExecutor
    {
        readonly BaseDbContext _context;
        Dictionary<Type, Action<BaseCommand>> _actions;

        public CommandChainExecutor(BaseDbContext context)
        {
            _context = context;
            _actions = new Dictionary<Type, Action<BaseCommand>>();
        }

        public ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> action) where TCommand : BaseCommand
        {
            _actions.Add(typeof(TCommand), (Action<BaseCommand>)action);
            return this;
        }

        public void ExecuteAll()
        {
            foreach (var action in _actions)
            {
                var command = (BaseCommand)Activator.CreateInstance(action.Key);
                action.Value(command);
            }
        }

        public void ExecuteAllWithTransaction()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                ExecuteAll();
                transaction.Commit();
            }
        }
    }
}
