namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class MissingClaimException(string claimName) : RequestException(
    $"Couldn't find user's '{claimName}' claim!",
    GeneralErrors.NotFound,
    HttpStatusCode.Unauthorized);