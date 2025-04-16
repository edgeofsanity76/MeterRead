using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MeterRead.Data.Repositories;

public interface IRepository<TEntity>
{
    DbContext Context { get; }
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetAsync(long id);

    /// <summary>
    /// Provides a way of searching a repository by providing a search expression
    /// </summary>
    /// <param name="filter">The search expression (ie, x => x.Id == entityId)</param>
    /// <param name="orderBy">The orderby expression</param>
    /// <param name="includes">Specify what entity relationships to include as defined by the key constraints in the database</param>
    /// <returns></returns>
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes);

    Task<int> SaveChangesAsync();
}