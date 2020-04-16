using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FilmReference.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FilmReferenceContext _dbContext;

        public GenericRepository(FilmReferenceContext dbContext) => 
            _dbContext = dbContext;

        public async Task<IEnumerable<T>> GetAll() => 
            await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate) => 
            await _dbContext.Set<T>().Where(predicate).ToListAsync();

        public IQueryable<T> GetAllQueryable() => 
            _dbContext.Set<T>();
       
        public async Task<T> GetById(int id) => 
            await _dbContext.Set<T>().FindAsync(id);

        public async Task Add(T model) =>
            await _dbContext.AddAsync(model);

        public Task Update(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
            return _dbContext.SaveChangesAsync();
        }
        
        public Task Save() =>
            _dbContext.SaveChangesAsync();
    }
}
