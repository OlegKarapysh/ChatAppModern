namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class GroupAlreadyExistsException() : RequestException(
    "Group with such name already exists!",
    GeneralErrors.InvalidNaming,
    HttpStatusCode.BadRequest);