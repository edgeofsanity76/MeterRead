using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MeterRead.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    DbContext Context { get; }
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetAsync(long id);

    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter, params string[] includes);

    Task<int> SaveChangesAsync();
}