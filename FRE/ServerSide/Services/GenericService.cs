using ServerSide.Repositories.Interfaces;
using ServerSide.Services.Interfaces;
using System.Linq.Expressions;

namespace ServerSide.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<T> CreateAsync(T entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _repository.Where(predicate);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _repository.DeleteAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
