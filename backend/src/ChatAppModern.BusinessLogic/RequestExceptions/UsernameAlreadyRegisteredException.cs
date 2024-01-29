namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class UsernameAlreadyRegisteredException() : RequestException(
    "This username is already registered!",
    AuthErrors.InvalidUsername,
    HttpStatusCode.BadRequest);