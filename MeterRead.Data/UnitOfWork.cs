using MeterRead.Data.Context;
using MeterRead.Data.Interfaces;
using MeterRead.Data.Repositories;

namespace MeterRead.Data;

public class UnitOfWork(MeterReadDbContext context) : IUnitOfWork
{
    public AccountRepository AccountRepository { get; } = new(context);
    public ReadingRepository ReadingRepository { get; } = new(context);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}