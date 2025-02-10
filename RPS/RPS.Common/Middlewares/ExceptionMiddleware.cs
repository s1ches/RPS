using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RPS.Common.Exceptions;

namespace RPS.Common.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (ex is ApplicationExceptionBase applicationException)
            {
                ex = applicationException;
                context.Response.StatusCode = (int)applicationException.StatusCode;   
                logger.LogInformation(ex.Message);
            }
            else
            {
                logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }


            var message = new { ex.Message };
            var serializedMessage = JsonSerializer.Serialize(message);
            await context.Response.WriteAsync(serializedMessage);
        }
    }
}