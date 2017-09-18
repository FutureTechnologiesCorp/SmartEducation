using System;

namespace FT.DataAccess.CQRS
{
    public interface ICommandExecutor<TCommand>
    {
        void Process(Action<TCommand> action);
    }
}
