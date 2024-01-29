namespace ChatAppModern.BusinessLogic.RequestExceptions;

public abstract class RequestException : Exception
{
    public ErrorDetails ErrorDetails { get; }
    public HttpStatusCode StatusCode { get; }

    protected RequestException(string message, string errorType, HttpStatusCode statusCode) : base(message)
    {
        ErrorDetails = new ErrorDetails(errorType, message);
        StatusCode = statusCode;
    }
}