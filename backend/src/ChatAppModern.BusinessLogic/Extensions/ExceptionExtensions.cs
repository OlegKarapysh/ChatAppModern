namespace ChatAppModern.BusinessLogic.Extensions;

public static class ExceptionExtensions
{
    public static (ErrorDetails, HttpStatusCode) GetErrorDetailsAndStatusCode(this Exception exception)
    {
        return exception switch
        {
            RequestException e => (e.ErrorDetails, e.StatusCode),
            _ => (new ErrorDetails(GeneralErrors.Internal, exception.Message), HttpStatusCode.InternalServerError)
        };
    }

    public static Result ToFailedResult(this Exception exception)
        => Result.Fail(new ExceptionalError(exception))!;

    public static Result<T> ToFailedResult<T>(this Exception exception)
        => Result.Fail(new ExceptionalError(exception))!.ToResult<T>()!;
}