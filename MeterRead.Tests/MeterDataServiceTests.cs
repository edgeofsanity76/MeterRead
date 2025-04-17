using MeterRead.Application.Models;
using MeterRead.Application.Services;
using MeterRead.Data.Entities;
using Moq;

namespace MeterRead.Tests;

public class MeterDataServiceTests(TestDataContext testDataContext) : IClassFixture<TestDataContext>
{
    [Fact]
    public void ShouldConstruct()
    {
        //Arrange
        var meterDataService = new MeterDataService(testDataContext.UnitOfWork.Object);
        //Act
        var result = meterDataService;
        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ShouldImportReadings()
    {
        //Arrange
        var meterDataService = new MeterDataService(testDataContext.UnitOfWork.Object);

        var meterReadings = new MeterReadings
        {
            new MeterReading { AccountId = 1, MeterReadingDate = DateTime.Parse("2023-01-01 00:00:00"), MeterReadingValue = "100" },
            new MeterReading { AccountId = 2, MeterReadingDate = DateTime.Parse("2023-01-02 00:00:00"), MeterReadingValue = "200" },
            new MeterReading { AccountId = 3, MeterReadingDate = DateTime.Parse("2023-01-03 00:00:00"), MeterReadingValue = "300" }
        };

        //Act
        await meterDataService.ImportReadingsAsync(meterReadings);

        //Assert
        testDataContext.UnitOfWork.Verify(u => u.ReadingRepository.InsertAsync(It.IsAny<Reading>()), Times.Exactly(3));
        testDataContext.UnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}