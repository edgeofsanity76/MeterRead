using MeterRead.Application.Exceptions;
using MeterRead.Application.Interfaces;
using MeterRead.Application.Models;

namespace MeterRead.Application.Services;

public class MeterDataParserService :  IMetaDataParserService
{
    public async Task<MeterReadings> ParseFileAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);

        var csvHeader = await reader.ReadLineAsync() ?? string.Empty;

        CheckHeaders(csvHeader);

        var readings = new MeterReadings();

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync() ?? string.Empty;
            if (TryParseLine(line, out var reading)) readings.Add(reading);
        }

        return readings;
    }

    private static bool TryParseLine(string line, out MeterReading meterReading)
    {
        try
        {
            var columns = line.Split(',');

            if (columns.Length < 3)
                throw new InvalidRowLengthException("The line must have 3 columns");

            meterReading = new MeterReading
            {
                AccountId = int.Parse(columns[0]),
                MeterReadingDate = DateTime.Parse(columns[1]),
                MeterReadingValue = columns[2].PadLeft(5, '0')
            };
        }
        catch (InvalidRowLengthException)
        {
            throw;
        }
        catch
        {
            //Log here
            meterReading = null!;
            return false;
        }

        return true;
    }

    private static void CheckHeaders(string csvHeader)
    {
        if (string.IsNullOrEmpty(csvHeader))
            throw new InvalidHeaderException("The header cannot be empty");

        var headers = csvHeader.Split(',');

        if (headers.Length < 3)
            throw new InvalidHeaderLengthException("The header must have 3 columns");
        if (headers[0] != "AccountId")
            throw new InvalidHeaderException("The first column must be AccountId");
        if (headers[1] != "MeterReadingDateTime")
            throw new InvalidHeaderException("The second column must be MeterReadingDateTime");
        if (headers[2] != "MeterReadValue")
            throw new InvalidHeaderException("The third column must be MeterReadValue");
    }
}