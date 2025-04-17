using MeterRead.Data.Context;
using MeterRead.Data.Entities;
using MeterRead.Data.Interfaces;
using MeterRead.Data.Repositories;

namespace MeterRead.Data;

public class UnitOfWork(MeterReadDbContext context) : IUnitOfWork
{
    public IRepository<Account> AccountRepository { get; } = new AccountRepository(context);
    public IRepository<Reading> ReadingRepository { get; } = new ReadingRepository(context);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}