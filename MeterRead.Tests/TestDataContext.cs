using System.Linq.Expressions;
using MeterRead.Data.Entities;
using MeterRead.Data.Interfaces;
using Moq;

namespace MeterRead.Tests;

public class TestDataContext
{
    public Mock<IUnitOfWork> UnitOfWork { get; set; } = new();

    private readonly Mock<IRepository<Account>> _accountRepository = new();
    private readonly Mock<IRepository<Reading>> _readingRepository = new();

    public TestDataContext()
    {
        UnitOfWork.Setup(u => u.AccountRepository).Returns(_accountRepository.Object);
        UnitOfWork.Setup(u => u.ReadingRepository).Returns(_readingRepository.Object);

        SetupAccountRepository();
        SetupReadingRepository();
    }

    private void SetupAccountRepository()
    {
        //Create list of accounts
        var accounts = new List<Account>
        {
            new() { AccountId = 1, FirstName = "Account 1", LastName = "Test"},
            new() { AccountId = 2, FirstName = "Account 2", LastName = "Test"},
            new() { AccountId = 3, FirstName = "Account 3", LastName = "Test"}
        };

        _accountRepository.Setup(a => a.Get(It.IsAny<Expression<Func<Account, bool>>>()))
            .Returns(accounts.AsQueryable());
    }

    private void SetupReadingRepository()
    {
        //Create list of readings
        var readings = new List<Reading>
        {
            new() { AccountId = 1, DateTime = "2023-01-01 00:00:00", Value = "100"},
            new() { AccountId = 2, DateTime = "2023-01-02 00:00:00", Value = "200"},
            new() { AccountId = 3, DateTime = "2023-01-03 00:00:00", Value = "300"}
        };
        _readingRepository.Setup(r => r.Get(It.IsAny<Expression<Func<Reading, bool>>>()))
            .Returns(readings.AsQueryable());
    }
}