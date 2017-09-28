using System;

namespace Core.CQRS
{
    public interface ICommandExecutor<TCommand>
    {
        void Process(Action<TCommand> action);
    }
}
