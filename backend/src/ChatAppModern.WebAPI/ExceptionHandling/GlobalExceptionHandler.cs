namespace ChatAppModern.WebAPI.ExceptionHandling;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);
        var (errorDetails, statusCode) = exception.GetErrorDetailsAndStatusCode();
        var problemDetails = new ProblemDetails
        {
            Detail = errorDetails.Message,
            Status = (int)statusCode,
            Title = errorDetails.Type
        };
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        
        return true;
    }
}