namespace FT.CQRS
{
    public interface IExecutor
    {
        IQueryExecutor<TQuery> GetQuery<TQuery>();
        ICommandExecutor<TCommand> GetCommand<TCommand>();
    }
}
