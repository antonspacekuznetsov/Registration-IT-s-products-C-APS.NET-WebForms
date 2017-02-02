using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RegITProducts.administator.Data;

namespace RegITProducts.administator.pages
{
    public class Controller<T>:IController<T> where T:class
    {
        private IUnitOfWork _unit=new UnitOfWork();
        private Repository<T> _repository;
        private bool _disposed;

        public Controller()
        {
            _repository = _unit.Repository<T>();
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> item = _repository.Table.ToList();
            return item;
        }

        public void Create(T entity)
        {
            _repository.Insert(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            T model = _repository.GetById(id);
            _repository.Delete(model);
        }
        public T GetById(int id)
        {
            T item = _repository.GetById(id);
            return item;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _unit.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}