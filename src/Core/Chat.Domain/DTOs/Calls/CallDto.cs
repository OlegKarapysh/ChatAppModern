namespace Chat.Domain.DTOs.Calls;

public class CallDto
{
    public string Id { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public string CallerUserName { get; set; } = string.Empty;
    public CallStatus Status { get; set; }
}