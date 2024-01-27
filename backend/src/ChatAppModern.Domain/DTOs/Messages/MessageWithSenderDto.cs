namespace ChatAppModern.Domain.DTOs.Messages;

public class MessageWithSenderDto : MessageDto
{
    public string UserName { get; set; } = string.Empty;
}