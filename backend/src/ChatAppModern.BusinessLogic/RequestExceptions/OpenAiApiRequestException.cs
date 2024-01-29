namespace ChatAppModern.BusinessLogic.RequestExceptions;

public sealed class OpenAiApiRequestException(string message)
    : RequestException(message, OpenAiErrors.RequestFailed, HttpStatusCode.BadRequest);