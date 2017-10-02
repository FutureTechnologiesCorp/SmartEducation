using Core.DataAccess;

namespace Core.DI
{
    public interface IAmbientContext
    {
        IUnitOfWork UnitOfWork { get; }
        TObject ResolveObject<TObject>();
    }
}
