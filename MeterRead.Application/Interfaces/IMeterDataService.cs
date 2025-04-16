using MeterRead.Application.Models;

namespace MeterRead.Application.Interfaces;

public interface IMeterDataService
{
    Task ImportReadings(IEnumerable<MeterReading> readings);
    IEnumerable<InvalidMeterReading> ValidateReadings(List<MeterReading> readings);
}