using System.Collections.Generic;
using System.Linq;

namespace FT.DataAccess.Repository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> AsQueryable();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
