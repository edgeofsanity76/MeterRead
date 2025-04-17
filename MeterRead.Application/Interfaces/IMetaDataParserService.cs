using MeterRead.Application.Models;

namespace MeterRead.Application.Interfaces;

public interface IMetaDataParserService
{
    Task<MeterReadings> ParseFileAsync(Stream stream);
}