using MongoDB.Driver;
using System.Linq.Expressions;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Interfaces;

namespace WorkiomBackendDevTest.Repositories.Classes
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> GetByNameAsync(string name)
        {
            return await _collection.Find(e => e.Name == name).FirstOrDefaultAsync();
        }


        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<T>> GetPagedAsync(int skip, int limit)
        {
            return await _collection.Find(_ => true)
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();
        }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

    }

}
