using MeterRead.Application.Services;

namespace MeterRead.Application.Interfaces;

public interface IMetaDataParserService
{
    Task<MeterReadings> ParseFileAsync(Stream stream);
}