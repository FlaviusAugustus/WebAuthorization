using System.Linq.Expressions;
using Shared.Models;

namespace WebAppAuthorization.Persistence.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    void Add(TEntity entity);

    Task AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveByIdAsync(Guid id);
    Task SaveAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task<int> GetItemCount();
}