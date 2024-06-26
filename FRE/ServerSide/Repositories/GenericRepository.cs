using Microsoft.EntityFrameworkCore;
using ServerSide.Data;
using ServerSide.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ServerSide.Repositories

{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CafeteriaDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(CafeteriaDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var entityEntry = _dbSet.Update(entity);
            if (entityEntry.State == EntityState.Modified)
            {
                await SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
