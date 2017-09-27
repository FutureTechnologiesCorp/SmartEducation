using System;
namespace FT.Common
{
    public interface IAmbientContext
    {
        TQuery ResolveQuery<TQuery>();
        TCommand ResolveCommand<TCommand>();
    }
}
