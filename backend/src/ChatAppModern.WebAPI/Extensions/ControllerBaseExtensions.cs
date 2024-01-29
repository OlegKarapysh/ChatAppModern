namespace ChatAppModern.WebAPI.Extensions;

public static class ControllerBaseExtensions
{
    public static ObjectResult ProblemFromFailedResult(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("Cannot convert successful result to problem!");
        }

        if (result.Errors is null || !result.Errors.Any())
        {
            throw new ArgumentException("Cannot convert result with no errors to problem!");
        }

        if (result.Errors.First() is ExceptionalError { Exception: RequestException requestException })
        {
            var (errorDetails, statusCode) = requestException;
            return controller.Problem(detail: errorDetails.Message, statusCode: (int)statusCode, type: errorDetails.Type);
        }

        return controller.Problem(result.Errors.First().Message);
    }

    public static ObjectResult ProblemFromFailedResult<T>(this ControllerBase controller, Result<T> result)
    {
        return controller.ProblemFromFailedResult(result.ToResult()!);
    }
    
}