namespace MeterRead.Application.Models;

public record InvalidMeterReading : MeterReading
{
    public required string Reason { get; set; }
}