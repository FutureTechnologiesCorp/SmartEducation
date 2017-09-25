using System;

namespace FT.CQRS
{
    public interface ICommandChainExecutor
    {
        ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> action) where TCommand: BaseCommand;
        void ExecuteAll();
        void ExecuteAllWithTransaction();
    }
}
