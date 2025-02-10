using System.Net;

namespace RPS.Common.Exceptions;

public class ApplicationExceptionBase(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}