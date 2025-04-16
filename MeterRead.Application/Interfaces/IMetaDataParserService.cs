using MeterRead.Application.Models;

namespace MeterRead.Application.Interfaces;

public interface IMetaDataParserService
{
    Task<List<MeterReading>> ParseFileAsync(Stream stream);
}