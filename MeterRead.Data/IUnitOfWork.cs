using MeterRead.Data.Repositories;

namespace MeterRead.Data;

public interface IUnitOfWork
{
    AccountRepository AccountRepository { get; }
    ReadingRepository ReadingRepository { get; }
    Task SaveChangesAsync();
}