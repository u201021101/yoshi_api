using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Context;

namespace Yoshi.EntityLayer.Service
{
    public class EntityService<T> : IEntityService<T> where T : class, new()
    {
        #region Variables --------------------
        protected readonly ApplicationDbContext _context;
        #endregion
        #region Properties -------------------
        public IQueryable<T> Table
        {
            get
            {
                return _context.Set<T>();
            }
        }
        #endregion
        #region Constructor ------------------
        public EntityService()
        {
            this._context = new ApplicationDbContext();
        }
        #endregion
        #region IEntityService<T>
        public void Add(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
                _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }

            _context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> FindAll()
        {
            return Table;
        }

        public T Get(params object[] id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder message = new StringBuilder();

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        message.AppendLine(string.Format("{0}:{1}", entityValidationError.Entry.Entity, validationError.ErrorMessage));
                    }
                }

                throw new InvalidOperationException(message.ToString(), ex);
            }
        }
        #endregion
    }
}
