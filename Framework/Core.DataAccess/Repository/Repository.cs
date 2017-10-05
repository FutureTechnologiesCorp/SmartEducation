using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Core.DataAccess.Filters.FilteringCommonObjects;

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

        public T GetById(object keyValue)
        {
            return _dbSet.Find(keyValue);
        }

		public IEnumerable<T> GetAll()
		{
            IQueryable<T> query = _dbSet;
            return query.AsEnumerable();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet;
        }
        
        public IQueryable<T> ApplyFilterByFillteringSettings(FilterSettings<T> filteringSettings)
        {
            var res = _dbSet.ApplyFilterSettings<T>(filteringSettings);
            return res;
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
