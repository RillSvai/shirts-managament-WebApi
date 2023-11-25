using ShirtsManagament.API.Data;
using ShirtsManagament.API.Models;
using ShirtsManagament.API.Repositories.IRepositories;

namespace ShirtsManagament.API.Repositories
{
    public class ShirtRepository : Repository<Shirt>, IShirtRepository
    {
        private readonly ApplicationDbContext _db;
        public ShirtRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    }
}
