using MeterRead.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace MeterRead.Api.ExceptionHandler;

public class ExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var responseCode = exception switch
        {
            InvalidHeaderException
                or InvalidHeaderLengthException
                or InvalidRowLengthException
                or ParseException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError

        };

        httpContext.Response.Clear();
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = responseCode;

        var body = new
        {
            StatusCode = responseCode,
            exception.Message,
            Detail = exception.InnerException?.Message
        };

        httpContext.Response.WriteAsJsonAsync(body, cancellationToken: cancellationToken);
        return ValueTask.FromResult(true);
    }
}