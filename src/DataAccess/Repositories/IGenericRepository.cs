using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace FilmReference.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQueryable();
        Task<T> GetById(int id);
        Task Add(T model);
        Task Update(T model);
        //Task UpdateTracked(T model);
        Task Delete(T model);
        Task Save();
    }
}
