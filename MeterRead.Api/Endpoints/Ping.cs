namespace MeterRead.Api.Endpoints;

public static class Ping
{
    public static void Map(WebApplication app) => app.MapGet("ping", () => Task.FromResult(Results.Ok("Working")));
}