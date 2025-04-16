namespace MeterRead.Application.Models;

public record MeterReading
{
    public Guid ReadingId { get; set; } = Guid.NewGuid();
    public required int AccountId { get; set; }
    public DateTime MeterReadingDate { get; set; }
    public required string MeterReadingValue { get; set; }
}