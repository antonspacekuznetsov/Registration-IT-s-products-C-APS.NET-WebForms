using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegITProducts.administator.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ItProductsEntities _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(ItProductsEntities context)
        {
            this._context = context;
        }

        public UnitOfWork()
        {
            _context = new ItProductsEntities();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Repository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)_repositories[type];
        }
    }
}