using Core.DataAccess.Repository;

namespace Core.DataAccess
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        void SaveChanges();
    }
}
