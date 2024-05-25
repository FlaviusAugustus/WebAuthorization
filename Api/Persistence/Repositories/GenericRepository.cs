using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace WebAppAuthorization.Persistence.Repositories;

public class GenericRepository<TEntity>(WebAuthDbContext context) : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly WebAuthDbContext _context = context;

    public void Add(TEntity entity) =>
        _context.Set<TEntity>().Add(entity);

    public async Task AddRange(IEnumerable<TEntity> entities) =>
        await _context.Set<TEntity>().AddRangeAsync(entities);

    public void Update(TEntity entity) =>
        _context.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) =>
        _context.Set<TEntity>().Remove(entity);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

    public async Task RemoveByIdAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        if (entity is not null)
        {
            _context.Remove(entity);
        }
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id) =>
        await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await _context.Set<TEntity>().ToListAsync();

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) =>
        await _context.Set<TEntity>().Where(expression).ToListAsync();

    public async Task<int> GetItemCount() =>
        await _context.Set<TEntity>().CountAsync();
} 