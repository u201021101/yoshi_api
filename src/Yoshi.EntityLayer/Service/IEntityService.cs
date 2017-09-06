using System.Collections.Generic;

namespace Yoshi.EntityLayer.Service
{
    public interface IEntityService<T> where T : class, new()
    {
        IEnumerable<T> FindAll();
        T Get(params object[] id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void Save();
    }
}
