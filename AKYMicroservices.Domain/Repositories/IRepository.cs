using System.Linq.Expressions;
using AKYMicroservices.Domain.Entities;

namespace AKYMicroservices.Domain.Repositories;

public interface IRepository<TEntity, TPrimaryKey>  where TEntity : BaseEntity<TPrimaryKey>
{
    Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
    T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetListAsync(int pageSize, int pageNumber);
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity,CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(Expression predicate, CancellationToken cancellationToken = default);
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

