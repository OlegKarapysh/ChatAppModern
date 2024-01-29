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

    public static implicit operator Result(RequestException exception) => exception.ToFailedResult();

    public void Deconstruct(out ErrorDetails errorDetails, out HttpStatusCode statusCode)
    {
        (errorDetails, statusCode) = (ErrorDetails, StatusCode);
    }
}