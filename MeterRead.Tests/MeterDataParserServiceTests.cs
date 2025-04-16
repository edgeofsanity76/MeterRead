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
        //Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task ShouldThrowInvalidHeaderExceptionWhenHeaderIsInvalid()
    {
        //Arrange
        var meterDataParserService = new MeterRead.Application.Services.MeterDataParserService();
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "TestData_InvalidHeader.csv");
        await using var stream = new FileStream(filePath, FileMode.Open);
        //Act
        var exception = await Assert.ThrowsAsync<MeterRead.Application.Exceptions.InvalidHeaderException>(async () =>
            await meterDataParserService.ParseFileAsync(stream));
        //Assert
        Assert.NotNull(exception);
    }
}