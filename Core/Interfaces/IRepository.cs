using UserManagementTask.Core.Entities;
using System.Linq.Expressions;

namespace UserManagementTask.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id, Expression<Func<T, object>>[]? include);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteListAsync(List<int> ids);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate);
        void SaveInclude(T entity, params string[] includedProperties);
        Task SaveChangesAsync();
    }
}

