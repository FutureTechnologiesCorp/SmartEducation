using System;
namespace Core.Common
{
    public interface IAmbientContext
    {
        TQuery ResolveQuery<TQuery>();
        TCommand ResolveCommand<TCommand>();
    }
}
