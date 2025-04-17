using MeterRead.Application.Models;
using MeterRead.Application.Services;

namespace MeterRead.Application.Interfaces;

public interface IMeterDataService
{
    Task ImportReadingsAsync(MeterReadings readings);
}