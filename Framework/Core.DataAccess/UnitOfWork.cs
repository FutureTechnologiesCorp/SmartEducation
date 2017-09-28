using System;
using Core.DataAccess.Repository;

namespace Core.DataAccess
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        BaseDbContext _context;

        public UnitOfWork(BaseDbContext context)
        {
            _context = context;
        }

        public Repository<T> GetRepository<T>() where T: class
        {
            var repository = new Repository<T>(_context);
            return repository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}
    }
}
