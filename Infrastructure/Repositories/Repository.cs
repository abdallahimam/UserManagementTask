using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Linq.Expressions;
using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.Infrastructure.Data;

namespace UserManagementTask.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(predicate);
        }
        public IQueryable<T> GetQueryableByCriteria(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<T?> GetByIdAsync(int id, Expression<Func<T, object>>[]? include)
        {

            IQueryable<T> query = _dbSet.AsQueryable();

            if (include != null && include.Any())
            {
                foreach (var includeExpression in include)
                {
                    query = query.Include(includeExpression);
                }
            }

            return await query.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        public void SaveInclude(T entity, params string[] includedProperties)
        {
            var localEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
            EntityEntry entry;

            if (localEntity is null)
            {
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.ChangeTracker.Entries<T>().First(e => e.Entity.Id == entity.Id);
            }


            foreach (var property in entry.Properties)
            {
                if (includedProperties.Contains(property.Metadata.Name))
                {
                    property.IsModified = true;
                }
                else
                {
                    property.IsModified = false;
                }
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task DeleteListAsync(List<int> ids)
        {
            var entity = await _dbSet.Where(o => ids.Contains(o.Id)).ToListAsync();
            if (entity != null)
            {
                foreach (var item in entity)
                {
                    item.IsDeleted = true;
                }
                _dbSet.UpdateRange(entity);
            }
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll() => _dbSet;

    }
}

