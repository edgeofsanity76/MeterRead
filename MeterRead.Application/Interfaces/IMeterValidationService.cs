using MeterRead.Application.Services;

namespace MeterRead.Application.Interfaces;

public interface IMeterValidationService
{
    (MeterReadings validReadings, MeterReadings invalidReadings) ValidateReadings(MeterReadings readings);
}