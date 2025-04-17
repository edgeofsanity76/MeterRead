using MeterRead.Data.Entities;
using MeterRead.Data.Repositories;

namespace MeterRead.Data.Interfaces;

public interface IUnitOfWork
{
    IRepository<Account> AccountRepository { get; }
    IRepository<Reading> ReadingRepository { get; }
    Task SaveChangesAsync();
}