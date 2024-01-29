namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class WrongConversationTypeException() : RequestException(
    "Requested operation with this conversation type is not supported!",
    GeneralErrors.InvalidAction,
    HttpStatusCode.BadRequest);