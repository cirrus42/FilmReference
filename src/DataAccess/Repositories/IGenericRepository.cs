using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FilmReference.DataAccess.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        Task Add(T model);
        void Update(T model);
        void Delete(T model);
        Task Save();
    }
}
