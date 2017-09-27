using FT.DataAccess.Repository;

namespace FT.DataAccess
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        void SaveChanges();
    }
}
