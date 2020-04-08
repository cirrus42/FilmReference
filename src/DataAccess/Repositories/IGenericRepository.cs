using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace FilmReference.DataAccess.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        IEnumerable GetAll(Expression<Func<T, bool>> expression);
        T GetById(int id);
        void Add(T model);
        void Delete(T model);
        void Save();
    }
}
