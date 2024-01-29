namespace ChatAppModern.BusinessLogic.RequestExceptions;

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
}