using ShirtsManagament.API.Data;
using ShirtsManagament.API.Repositories.IRepositories;

namespace ShirtsManagament.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ShirtRepo = new ShirtRepository(db);
        }
        public IShirtRepository ShirtRepo { get; }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
