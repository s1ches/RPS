using System.Net;

namespace RPS.Common.Exceptions;

public class InfrastructureExceptionBase(string message)
    : ApplicationExceptionBase(message, HttpStatusCode.InternalServerError);