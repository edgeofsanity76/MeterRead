using MeterRead.Application.Models;
using MeterRead.Application.Services;

namespace MeterRead.Tests;

public class MeterDataValidationServiceTests(TestDataContext context) : IClassFixture<TestDataContext>
{
    [Fact]
    public void ShouldConstruct()
    {
        //Arrange
        var meterDataValidationService = new MeterDataValidationService(context.UnitOfWork.Object);

        //Act
        var result = meterDataValidationService;

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void ShouldValidateReadings()
    {
        //Arrange
        var meterDataValidationService = new MeterDataValidationService(context.UnitOfWork.Object);

        var meterReadings = new MeterReadings
        {
            new MeterReading { AccountId = 1, MeterReadingDate = DateTime.Parse("2023-01-01 00:00:00"), MeterReadingValue = "100" },
            new MeterReading { AccountId = 2, MeterReadingDate = DateTime.Parse("2023-01-02 00:00:00"), MeterReadingValue = "200" },
            new MeterReading { AccountId = 3, MeterReadingDate = DateTime.Parse("2023-01-03 00:00:00"), MeterReadingValue = "300" },
            new MeterReading { AccountId = 1, MeterReadingDate = DateTime.Parse("2024-01-01 00:00:00"), MeterReadingValue = "400" },
            new MeterReading { AccountId = 2, MeterReadingDate = DateTime.Parse("2024-01-02 00:00:00"), MeterReadingValue = "500" },
            new MeterReading { AccountId = 3, MeterReadingDate = DateTime.Parse("2024-01-03 00:00:00"), MeterReadingValue = "600" }
        };


        //Act
        var (validReadings, invalidReadings) = meterDataValidationService.ValidateReadings(meterReadings);
        //Assert
        Assert.NotNull(validReadings);
        Assert.NotNull(invalidReadings);
        Assert.Equal(3, validReadings.Count);
        Assert.Equal(3, invalidReadings.Count);
    }
}