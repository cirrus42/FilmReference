using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace FilmReference.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FilmReferenceContext _dbContext;

        public GenericRepository(FilmReferenceContext dbContext) => 
            _dbContext = dbContext;

        public IQueryable<T> GetAll() =>
            _dbContext.Set<T>();

        public IEnumerable GetAll(Expression<Func<T, bool>> expression) =>
           _dbContext.Set<T>().Where(expression);

        public T GetById(int id) =>
            _dbContext.Set<T>().Find(id);

        public void Add(T model) =>
            _dbContext.Add(model);

        public void Delete(T model) =>
            _dbContext.Remove(model);

        public void Save() =>
            _dbContext.SaveChanges();
    }
}
