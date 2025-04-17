using MeterRead.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeterRead.Api.Endpoints;

public static class Meter
{
    public static void Map(WebApplication app)
    {
        app.MapPost("meter-reading-uploads", async ([FromServices] IMeterDataService meterDataService, IMetaDataParserService meterDataParserServiceService, IMeterValidationService meterValidationService, IFormFile file) =>
        {
            if (file.Length == 0)
                return Results.BadRequest("The file must have content");

            var readings = await meterDataParserServiceService.ParseFileAsync(file.OpenReadStream());
            var validatedReadings = meterValidationService.ValidateReadings(readings);
            await meterDataService.ImportReadingsAsync(validatedReadings.validReadings);

            return Results.Ok(new { ImportedReadings = validatedReadings.validReadings, InvalidReadings = validatedReadings.invalidReadings });
        }).Accepts<IFormFile>("multipart/form-data").DisableAntiforgery();
    }
}