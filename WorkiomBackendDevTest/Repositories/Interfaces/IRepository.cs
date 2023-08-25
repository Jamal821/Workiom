using System.Linq.Expressions;
using WorkiomBackendDevTest.Entities;

namespace WorkiomBackendDevTest.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<T> GetByNameAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetPagedAsync(int skip, int limit);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filterExpression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);

    }
}
