using MeterRead.Data.Repositories;

namespace MeterRead.Data.Interfaces;

public interface IUnitOfWork
{
    AccountRepository AccountRepository { get; }
    ReadingRepository ReadingRepository { get; }
    Task SaveChangesAsync();
}