using Fcg.Domain.Exceptions;
using Fcg.Domain.Exceptions.Responses;
using System.Net;
using System.Text.Json;

namespace Fcg.WebApi.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu uma exceção: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetResponseStatusCode(exception);

        var jsonResponse = JsonSerializer.Serialize(
            new ErrorResponse(
                context.Response.StatusCode,
                exception.Message,
                new Dictionary<string, string[]>()));

        return context.Response.WriteAsync(jsonResponse);
    }

    private static int GetResponseStatusCode(Exception exception) => exception switch
    {
        NotFoundException => (int)HttpStatusCode.NotFound,
        BussinessException => (int)HttpStatusCode.BadRequest,
        AuthenticationException => (int)HttpStatusCode.Unauthorized,
        ConflictException => (int)HttpStatusCode.Conflict,
        _ => (int)HttpStatusCode.InternalServerError,
    };
}