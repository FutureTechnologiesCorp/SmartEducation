using System;
using Core.DataAccess;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.CQRS
{
    public class CommandExecutor<TCommand> : ICommandExecutor<TCommand>
    {
        TCommand _command;
        readonly BaseDbContext _context;
        IDbContextTransaction _transaction;

        public CommandExecutor(TCommand command, BaseDbContext context)
        {
            _command = command;
            _context = context;
        }

        public void Process(Action<TCommand> action)
        {
            action(_command);
        }

        public void ProcessWithTransaction(Action<TCommand> action)
        {
            try
            {
                using (_transaction = _context.Database.BeginTransaction())
                {
                    action(_command);
                    _transaction.Commit();
                }
            }
            catch
            {
                //todo: set error log
                _transaction.Rollback();
            }
        }
    }
}
