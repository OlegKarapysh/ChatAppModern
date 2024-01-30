namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class InvalidPasswordException() : RequestException(
    "Invalid password!",
    AuthErrors.InvalidPassword,
    HttpStatusCode.BadRequest);