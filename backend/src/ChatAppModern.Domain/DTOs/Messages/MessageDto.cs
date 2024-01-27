namespace ChatAppModern.Domain.DTOs.Messages;

public class MessageDto : MessageBasicInfoDto
{
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public bool IsAiAssisted { get; set; }
    public Guid SenderId { get; set; }
    public Guid? DialogChatId { get; set; }
    public Guid? GroupChatId { get; set; }
}