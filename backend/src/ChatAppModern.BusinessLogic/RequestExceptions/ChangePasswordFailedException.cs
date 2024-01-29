namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class ChangePasswordFailedException() : RequestException(
    "Failed to change user's password!",
    AuthErrors.InvalidPassword,
    HttpStatusCode.BadRequest);