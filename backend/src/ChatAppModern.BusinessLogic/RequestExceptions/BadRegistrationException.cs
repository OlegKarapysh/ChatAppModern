namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class BadRegistrationException() : RequestException(
    "Registration failed!",
    AuthErrors.InvalidEmailOrPassword,
    HttpStatusCode.BadRequest);