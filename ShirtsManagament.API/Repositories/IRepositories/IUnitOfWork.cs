namespace ShirtsManagament.API.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IShirtRepository ShirtRepo { get; }
        public Task SaveAsync();
    }
}
