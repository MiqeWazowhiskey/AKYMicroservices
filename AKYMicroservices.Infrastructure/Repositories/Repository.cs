using System.Linq.Expressions;
using AKYMicroservices.Domain.Entities;
using AKYMicroservices.Domain.Enums;
using AKYMicroservices.Domain.Repositories;
using AKYMicroservices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AKYMicroservices.Infrastructure.Repositories;


public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
{
    private readonly ApplicationDbContext dbContext;
    public Repository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        return await Entities.Where(e=>e.Status!=EntityStatus.Deleted).FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);
    }

    public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
    {
        return queryMethod(Entities);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await Entities.Where(e=>e.Status!= EntityStatus.Deleted).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(int pageSize,int pageNumber)
    {
        var skipNumber = (pageNumber - 1) * pageSize;
        return await Entities.Where(e=>e.Status != EntityStatus.Deleted
        ).Skip(skipNumber).Take(pageSize).ToListAsync();
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        
        await DeleteAsync(entity, cancellationToken);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression predicate, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.Where(e=>e.Status!=EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        return entity;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    private IQueryable<TEntity> Entities => dbContext.Set<TEntity>();
}

