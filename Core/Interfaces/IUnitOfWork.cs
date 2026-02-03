using UserManagementTask.Core.Entities;

namespace UserManagementTask.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }

        Task<int> SaveChangesAsync();
    }
}

