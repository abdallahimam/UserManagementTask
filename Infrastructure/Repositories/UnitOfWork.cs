using Microsoft.EntityFrameworkCore.Storage;
using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.Infrastructure.Data;

namespace UserManagementTask.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private IRepository<User> _UserRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> UserRepository => _UserRepository ??= new Repository<User>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}

