namespace ChatAppModern.BusinessLogic.RequestExceptions;

public class InvalidPasswordException() : RequestException(
    "Invalid password.",
    AuthErrors.InvalidPassword,
    HttpStatusCode.BadRequest);