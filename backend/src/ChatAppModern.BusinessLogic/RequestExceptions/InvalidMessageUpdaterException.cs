namespace ChatAppModern.BusinessLogic.RequestExceptions;

public class InvalidMessageUpdaterException() : RequestException(
    "Only the user who sent the message can change it!",
    GeneralErrors.InvalidAction,
    HttpStatusCode.BadRequest);