using System;

namespace FT.CQRS
{
    public interface ICommandExecutor<TCommand>
    {
        void Process(Action<TCommand> action);
    }
}
