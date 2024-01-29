namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class RefreshTokenExpiredException() : RequestException(
    "Refresh token expired!",
    AuthErrors.TokenExpired,
    HttpStatusCode.Forbidden);