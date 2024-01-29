namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class EntityNotFoundException : RequestException
{
    public EntityNotFoundException(string entityName) : base(
        $"'{entityName}' is not found",
        GeneralErrors.NotFound,
        HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(string entityName, string property) : base(
        $"'{entityName}' is not found by '{property}' ",
        GeneralErrors.NotFound,
        HttpStatusCode.NotFound)
    {
    }
}