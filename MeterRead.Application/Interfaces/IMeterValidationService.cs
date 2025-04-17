using MeterRead.Application.Models;

namespace MeterRead.Application.Interfaces;

public interface IMeterValidationService
{
    (MeterReadings validReadings, List<InvalidMeterReading> invalidReadings) ValidateReadings(MeterReadings readings);
}