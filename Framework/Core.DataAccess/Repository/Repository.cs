using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attributes = prop.GetCustomAttributes(true);
                if (attributes.Any()){
                    foreach (var attribute in attributes)
                    {
                        if(attribute.GetType() == typeof(KeyAttribute))
                        {
                            return _dbSet.FirstOrDefault(item => item.GetType().GetProperty(prop.Name).Name == prop.Name
                                                              && item.GetType().GetProperty(prop.Name).GetValue(item, null).Equals(keyValue));
                        }
                    }
                }
            }

            return null;
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
