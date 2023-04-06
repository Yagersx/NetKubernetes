using System.Net;
using Newtonsoft.Json;

namespace NetKubernates.Middleware;

public class MiddlewareManager
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareManager> _logger;

    public MiddlewareManager(RequestDelegate next, ILogger<MiddlewareManager> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await ManagerExceptionAsync(context, exception, _logger);
        }
    }

    private async Task ManagerExceptionAsync(HttpContext context, Exception exception, ILogger<MiddlewareManager> logger)
    {
        object? error = null;

        switch (exception)
        {
            case MiddlewareException me:
                logger.LogError(exception, "Ups, error.");
                error = me.Error;
                context.Response.StatusCode = (int)me.Code;
                break;
            case Exception e:
                logger.LogError(exception, "Ups, error de servidor.");
                error = string.IsNullOrWhiteSpace(exception.Message) ? "Error" : exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";
        var resultados= string.Empty;
        if (error != null)
        {
            resultados = JsonConvert.SerializeObject(new { error });
        }

        await context.Response.WriteAsync(resultados);
    }
}