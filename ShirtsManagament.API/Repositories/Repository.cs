using Microsoft.EntityFrameworkCore;
using ShirtsManagament.API.Data;
using ShirtsManagament.API.Repositories.IRepositories;
using System.Linq.Expressions;

namespace ShirtsManagament.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public virtual void Delete(T? item)
        {
            if (item is not null) 
            {
                _dbSet.Remove(item);
            }
            
        }
        public virtual async Task DeleteAsync(int id)
        {
            T? item = await _dbSet.FindAsync(id);
            if (item is not null)
            {
                _dbSet.Remove(item);
            }
        }

        public virtual async Task<bool> ExistAsync(int id, bool isTracked = false)
        {
            T? item =  await _dbSet.FindAsync(id);
            if (item is not null && !isTracked) 
            {
                _db.Entry(item!).State = EntityState.Detached;
            }
            return item is not null;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter is not null) 
            {
                query = query.Where(filter);
            }
            return query;
        }
        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool isTracked = false)
        {
            IQueryable<T> query = isTracked ? _dbSet : _dbSet.AsNoTracking();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetById<TIdentifier>(TIdentifier id, bool isTracked = false)
        {
            T? item = await _dbSet.FindAsync(id);
            if (item is not null && !isTracked)
            {
                _db.Entry(item!).State = EntityState.Detached;
            }
            return item;
        }

        public virtual async Task InsertAsync(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
