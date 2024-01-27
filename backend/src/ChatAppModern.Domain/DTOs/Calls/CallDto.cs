namespace ChatAppModern.Domain.DTOs.Calls;

public class CallDto
{
    public string Id { get; set; } = string.Empty;
    public Guid DialogChatId { get; set; }
    public string CallerUserName { get; set; } = string.Empty;
    public CallStatus Status { get; set; }
}