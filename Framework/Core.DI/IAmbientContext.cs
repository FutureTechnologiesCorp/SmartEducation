namespace Core.DI
{
    public interface IAmbientContext
    {
        TObject ResolveObject<TObject>();
    }
}
