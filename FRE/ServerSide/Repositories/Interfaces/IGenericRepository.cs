using System.Linq.Expressions;

namespace ServerSide.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> UpdateAsync(T entity);
        Task<T> CreateAsync(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
        Task<List<T>> GetAllAsync();
    }
}
