using MeterRead.Application.Models;
using MeterRead.Application.Services;

namespace MeterRead.Application.Interfaces;

public interface IMeterValidationService
{
    (MeterReadings validReadings, List<InvalidMeterReading> invalidReadings) ValidateReadings(MeterReadings readings);
}