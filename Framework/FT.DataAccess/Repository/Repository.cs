using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FT.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T: class
    {
        BaseDbContext _context;

        public Repository(BaseDbContext context)
        {
            _context = context;
        }

		public IEnumerable<T> GetAll()
		{
            IQueryable<T> query = GetDbSet();
            return query.AsEnumerable<T>();
		}

        public IQueryable<T> AsQueryable()
        {
            return GetDbSet();
        }
        
        public void Create(T item)
        {
            GetDbSet().Add(item);
        }

        public void Delete(T item)
        {
            GetDbSet().Remove(item);
        }

        public void Update(T item)
        {
            GetDbSet().Update(item);
        }

        private DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }
    }
}
