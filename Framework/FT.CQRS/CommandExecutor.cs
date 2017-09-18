using System;
namespace FT.CQRS
{
    public class CommandExecutor<TCommand>: ICommandExecutor<TCommand>
    {
        private TCommand _command;
        
        public CommandExecutor(TCommand command)
        {
            _command = command;
        }

        public void Process(Action<TCommand> action)
        {
            action(_command);
        }
    }
}
