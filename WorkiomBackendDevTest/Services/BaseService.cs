using System.Linq.Expressions;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Classes;
using WorkiomBackendDevTest.Repositories.Interfaces;

namespace WorkiomBackendDevTest.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<T>> GetPagedAsync(int page, int pageSize)
        {
            return await _repository.GetPagedAsync(page < 1 || pageSize < 1 ? 0 : (page - 1) * pageSize, pageSize);    
        }
        public async Task<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _repository.FindAsync(filterExpression);
        }

        public async Task CreateAsync(T entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
