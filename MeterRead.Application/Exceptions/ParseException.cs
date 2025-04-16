namespace MeterRead.Application.Exceptions;

public class ParseException(string message, Exception innerException) : Exception(message, innerException);