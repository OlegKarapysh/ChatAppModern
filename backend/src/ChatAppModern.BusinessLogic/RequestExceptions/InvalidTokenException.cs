namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class InvalidTokenException(string tokenType) : RequestException(
    $"{tokenType} token is not valid!",
    AuthErrors.InvalidToken,
    HttpStatusCode.Forbidden);