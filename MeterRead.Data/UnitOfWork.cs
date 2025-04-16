using MeterRead.Data.Context;
using MeterRead.Data.Repositories;

namespace MeterRead.Data;

public class UnitOfWork(MeterReadDbContext context) : IUnitOfWork
{
    public AccountRepository AccountRepository { get; } = new AccountRepository(context);
    public ReadingRepository ReadingRepository { get; } = new ReadingRepository(context);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}