using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FilmReference.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FilmReferenceContext _dbContext;

        public GenericRepository(FilmReferenceContext dbContext) => 
            _dbContext = dbContext;

        //public async Task<IEnumerable>> GetAll() =>
        //    await _dbContext.Set<T>().ToListAsync();

        //public IEnumerable GetAll(Expression<Func<T, bool>> expression) =>
        //   _dbContext.Set<T>().Where(expression);
        public async Task<IEnumerable<T>> GetAll() => 
            await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate) => 
            await _dbContext.Set<T>().Where(predicate).ToListAsync();
        
        public T GetById(int id) =>
            _dbContext.Set<T>().Find(id);

        public async Task Add(T model) =>
            await _dbContext.AddAsync(model);

        public void Update(T model) =>
            _dbContext.Update(model);

        public void Delete(T model) =>
            _dbContext.Remove(model);

        public Task Save() =>
            _dbContext.SaveChangesAsync();
    }
}
