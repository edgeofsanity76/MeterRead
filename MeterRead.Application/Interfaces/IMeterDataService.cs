using MeterRead.Application.Models;

namespace MeterRead.Application.Interfaces;

public interface IMeterDataService
{
    Task ImportReadingsAsync(MeterReadings readings);
}