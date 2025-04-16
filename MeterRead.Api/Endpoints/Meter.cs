using MeterRead.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeterRead.Api.Endpoints;

public static class Meter
{
    public static void Map(WebApplication app)
    {
        app.MapPost("meter-reading-uploads", async ([FromServices] IMeterDataService meterDataService, IMetaDataParserService meterDataParserServiceService, IFormFile file) =>
        {
            if (file.Length == 0)
                return Results.BadRequest("The file must have content");

            var readings = await meterDataParserServiceService.ParseFileAsync(file.OpenReadStream());
            var invalidReadings = meterDataService.ValidateReadings(readings.ToList());
            var validReadings = readings.Where(x => invalidReadings.All(y => y.ReadingId != x.ReadingId)).ToList();
            await meterDataService.ImportReadings(validReadings);

            return Results.Ok(new { ImportedReadings = validReadings, InvalidReadings = invalidReadings });
        }).Accepts<IFormFile>("multipart/form-data").DisableAntiforgery();
    }
}