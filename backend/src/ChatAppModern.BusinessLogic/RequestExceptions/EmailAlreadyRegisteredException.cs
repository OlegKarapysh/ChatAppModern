namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class EmailAlreadyRegisteredException() : RequestException(
    "Email is already registered. Try another one",
    AuthErrors.InvalidEmail,
    HttpStatusCode.BadRequest);