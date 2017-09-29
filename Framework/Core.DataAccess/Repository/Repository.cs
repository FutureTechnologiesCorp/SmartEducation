using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T: class
    {
        private BaseDbContext _context;

        private DbSet<T> _dbSet => _context.Set<T>();

        public Repository(BaseDbContext context)
        {
            _context = context;
        }

		public IEnumerable<T> GetAll()
		{
            IQueryable<T> query = _dbSet;
            return query.AsEnumerable<T>();
		}

        public IQueryable<T> AsQueryable()
        {
            return _dbSet;
        }
        
        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
