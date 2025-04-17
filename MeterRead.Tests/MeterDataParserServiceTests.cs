namespace MeterRead.Tests;

public class MeterDataParserService
{
    [Fact]
    public void ShouldConstruct()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();

        //Act
        var result = meterDataParserService;
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ShouldParseFile()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "TestData_Valid.csv");
        await using var stream = new FileStream(filePath, FileMode.Open);
        //Act
        var result = await meterDataParserService.ParseFileAsync(stream);
        var expectedDataTime = DateTime.Parse("22/04/2019 09:24");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal(2344, result[0].AccountId);
        Assert.Equal(expectedDataTime, result[0].MeterReadingDate);
        Assert.Equal("01002", result[0].MeterReadingValue);
    }

    [Fact]
    public async Task ShouldThrowInvalidHeaderExceptionWhenHeaderLengthIsInvalid()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "TestData_InvalidHeaderLength.csv");
        await using var stream = new FileStream(filePath, FileMode.Open);
        //Act
        var exception = await Assert.ThrowsAsync<Application.Exceptions.InvalidHeaderException>(async () =>
            await meterDataParserService.ParseFileAsync(stream));
        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task ShouldThrowInvalidHeaderExceptionWhenHeaderIsMissing()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "TestData_MissingHeader.csv");
        await using var stream = new FileStream(filePath, FileMode.Open);
        //Act
        var exception = await Assert.ThrowsAsync<Application.Exceptions.InvalidHeaderException>(async () =>
            await meterDataParserService.ParseFileAsync(stream));
        //Assert
        Assert.NotNull(exception);
    }


    [Fact]
    public async Task ShouldThrowInvalidRowLengthExceptionWhenRowLengthIsInvalid()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "TestData_InvalidRowLength.csv");
        await using var stream = new FileStream(filePath, FileMode.Open);
        //Act
        var exception = await Assert.ThrowsAsync<Application.Exceptions.InvalidRowLengthException>(async () =>
            await meterDataParserService.ParseFileAsync(stream));
        //Assert
        Assert.NotNull(exception);
    }
}