using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Faly.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        ProblemDetailsFactory problemDetailsFactory
    )
    {
        _next = next;
        _logger = logger;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");

            var problemDetails = _problemDetailsFactory.CreateProblemDetails(
                httpContext,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Sunucu hatası.",
                detail: "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                instance: httpContext.Request.Path
            );

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/problem+json";

            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problemDetails);
        }
    }
}
