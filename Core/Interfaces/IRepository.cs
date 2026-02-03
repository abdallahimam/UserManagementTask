using UserManagementTask.Core.Entities;
using System.Linq.Expressions;

namespace UserManagementTask.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}

