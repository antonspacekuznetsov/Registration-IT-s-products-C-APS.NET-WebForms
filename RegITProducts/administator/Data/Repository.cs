using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
namespace RegITProducts.administator.Data
{
    public class Repository<T>  where T: class
    {
        private ItProductsEntities _context;
        private IDbSet<T> _entities;
        string errorMessage = string.Empty;

        public Repository(ItProductsEntities _db)
        {
            this._context = _db;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            if(entity==null)
                throw new ArgumentNullException("entity");
            this.Entities.Add(entity);
            this._context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            this._context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if(entity==null)
                throw new ArgumentNullException("entity");
            this.Entities.Remove(entity);
            this._context.SaveChanges();
        }



        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {

            get{
            if(_entities==null)
            _entities=_context.Set<T>();
            return _entities;

            }
        }
    }
}