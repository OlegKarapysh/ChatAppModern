namespace ChatAppModern.BusinessLogic.RequestExceptions;

public class InvalidEmailException() : RequestException(
    "Invalid email address!",
    AuthErrors.InvalidEmail,
    HttpStatusCode.BadRequest);