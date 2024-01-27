namespace ChatAppModern.Domain.DTOs.Messages;

public class MessageForAssistDto
{
    public Guid Id { get; set; }
    public string ReceiverUserName { get; set; } = string.Empty;
}