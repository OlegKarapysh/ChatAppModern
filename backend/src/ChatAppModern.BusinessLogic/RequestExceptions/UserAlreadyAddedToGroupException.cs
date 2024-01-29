namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class UserAlreadyAddedToGroupException() : RequestException(
    "This user is already added to one of your groups!",
    GeneralErrors.InvalidAction,
    HttpStatusCode.BadRequest);