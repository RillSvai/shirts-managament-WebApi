using System.Linq.Expressions;

namespace ShirtsManagament.API.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll(Expression<Func<T,bool>>? filter = null);
        public Task<T?> GetAsync(Expression<Func<T,bool>>? filter = null, bool isTracked = false);
        public Task<T?> GetByIdAsync<TIdentifier>(TIdentifier id, bool isTracked = false);
        public Task InsertAsync(T item);
        public Task<bool> ExistAsync(int id, bool isTracked = false);
        public void Update(T item);
        public void Delete(T? item);
        public Task DeleteAsync(int id);
    }
}
